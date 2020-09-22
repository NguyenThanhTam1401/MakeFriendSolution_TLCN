using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.Enum;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Common;
using System.Net.Http.Headers;
using System.IO;
using MakeFriendSolution.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IMailService _mailService;
        private readonly IConfiguration _config;
        private LoginInfo _loginInfo = new LoginInfo();

        public UsersController(MakeFriendDbContext context, IStorageService storageService, IMailService mailService, IConfiguration config)
        {
            _context = context;
            _storageService = storageService;
            _mailService = mailService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("CodeValidation")]
        public async Task<IActionResult> CodeValidation([FromForm] ForgotPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest(new
                {
                    Message = "Mật khẩu không được phép để trống."
                });
            }
            var user = await _context.Users.Where(x => x.Email == request.Email.Trim()).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy user với email: " + request.Email
                });
            }

            if (DateTime.Now > user.PasswordForgottenPeriod)
            {
                return BadRequest(new
                {
                    Message = "Mã đã hết hạn, vui lòng xác nhận lại email để nhận mã mới!"
                });
            }

            if (user.NumberOfPasswordConfirmations <= 0)
            {
                return BadRequest(new
                {
                    Message = "Số lần xác nhận đã hết, vui lòng xác nhận lại email sau " + user.PasswordForgottenPeriod.ToShortTimeString()
                });
            }

            if (user.PasswordForgottenCode != request.Code.Trim())
            {
                try
                {
                    user.NumberOfPasswordConfirmations--;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return StatusCode(501, new
                    {
                        Message = e.Message
                    });
                }

                return BadRequest(new
                {
                    Message = "Mã xác nhận không chính xác, số lần còn lại: " + user.NumberOfPasswordConfirmations
                });
            }

            user.PassWord = request.NewPassword;

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(501, new
                {
                    Message = e.Message
                });
            }
            return Ok(new
            {
                Message = "Mật khẩu đã được cập nhật, vui lòng đăng nhập."
            });
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] string email)
        {
            var user = await _context.Users.Where(a => a.Email == email.Trim()).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy user với email: " + email
                });
            }

            if (DateTime.Now < user.PasswordForgottenPeriod)
            {
                string time = user.PasswordForgottenPeriod.ToShortTimeString();
                return BadRequest(new
                {
                    Message = "Vui lòng nhập mã xác nhận trong email của bạn. Để nhận mã mới vui lòng xác nhận lại email sau " + time
                });
            }

            Random random = new Random();
            user.PasswordForgottenCode = random.Next(1000, 9999).ToString();
            user.PasswordForgottenPeriod = DateTime.Now.AddMinutes(15);
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(501, new
                {
                    Message = e.Message
                });
            }

            LoginInfo loginInfo = new LoginInfo()
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                Message = user.PasswordForgottenCode
            };

            MailClass mailClass = this.GetMailForgotPasswordObject(loginInfo);
            string result = await _mailService.SendMail(mailClass);

            if (result == MessageMail.MailSent)
            {
                return Ok(new
                {
                    Message = "Mã xác nhận đã được gửi vào mail của bạn, nhập mã để thay đổi mật khẩu, mã sẽ hết hiệu lực sau " + user.PasswordForgottenPeriod.ToShortTimeString()
                });
            }
            else
            {
                return BadRequest(new
                {
                    Message = result
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("confirmMail")]
        public async Task<IActionResult> ConfirmMail(Guid userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId.ToString())) return BadRequest(new
                {
                    Message = "Invalid userId"
                });

                LoginInfo loginInfo = new LoginInfo()
                {
                    UserId = userId
                };

                loginInfo = await this.CheckRecordExistence(loginInfo);

                if (loginInfo == null)
                    return BadRequest(new
                    {
                        Message = MessageMail.InvalidUser
                    });
                else
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user == null)
                    {
                        return NotFound(new
                        {
                            Message = "Can not find user with id = " + userId
                        });
                    }
                    user.Status = EUserStatus.Active;

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Message = "Mail Confirmed"
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(501, new
                {
                    Message = e.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("reconfirmMail")]
        public async Task<IActionResult> ReconfirmMail(string username)
        {
            var appUser = await _context.Users.Where(x => x.UserName == username.Trim()).FirstOrDefaultAsync();

            if (appUser == null)
            {
                return NotFound("Can not find user by username = " + username);
            }

            if (appUser.Status != EUserStatus.IsVerifying)
            {
                return BadRequest(new
                {
                    Message = "User has been confirmed, please login"
                });
            }

            var info = new LoginInfo()
            {
                Email = appUser.Email,
                FullName = appUser.FullName,
                UserId = appUser.Id
            };

            MailClass mailClass = this.GetMailObject(info);
            string result = await _mailService.SendMail(mailClass);

            if (result == MessageMail.MailSent)
            {
                return Ok(new
                {
                    Message = MessageMail.VerifyMail
                });
            }
            else return BadRequest(new
            {
                Message = result
            });
        }

        // GET: api/Users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Can not find user by id = " + userId);
            var respone = new UserResponse(user);
            try
            {
                byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(user.AvatarPath)}");
                respone.AvatarPath = Convert.ToBase64String(imageBits);
                respone.HasAvatar = true;
            }
            catch
            {
                respone.HasAvatar = false;
                respone.AvatarPath = user.AvatarPath;
            }
            var data = this.GetDataFromToken();
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var user = await _context.Users
                .Where(x => x.Email == request.Email.Trim()).FirstOrDefaultAsync();

            if (user == null)
            {
                var message = "Can not find User with UserName is " + request.Email;
                return BadRequest(message);
            }
            if (user.PassWord != request.Password.Trim())
            {
                var message = "Password is not correct!";
                return BadRequest(message);
            }

            if (user.Status == EUserStatus.IsVerifying)
            {
                var info = new LoginInfo()
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    IsMailConfirmed = false,
                    Message = MessageMail.VerifyMail,
                    UserId = user.Id,
                    UserName = user.UserName
                };

                return BadRequest(new
                {
                    Message = MessageMail.VerifyMail
                });
            }

            if (user.Status == EUserStatus.Inactive)
            {
                return BadRequest(new
                {
                    Message = "Your account has been locked!"
                });
            }

            var userResponse = new UserResponse(user);

            userResponse.Token = this.GenerateJSONWebToken(user);

            // Get image code if available
            try
            {
                byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(user.AvatarPath)}");
                userResponse.AvatarPath = Convert.ToBase64String(imageBits);
                userResponse.HasAvatar = true;
            }
            catch
            {
                userResponse.HasAvatar = false;
                userResponse.AvatarPath = user.AvatarPath;
            }

            return Ok(userResponse);
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromForm] LoginInfo request)
        {
            _loginInfo = new LoginInfo();
            LoginInfo loginInfo = new LoginInfo();

            loginInfo = await this.CheckRecordExistence(request);
            _loginInfo = loginInfo;
            if (loginInfo == null)
            {
                var user = new AppUser()
                {
                    CreatedAt = DateTime.Now,
                    Email = request.Email,
                    UserName = request.UserName,
                    FullName = request.FullName,
                    PassWord = request.Password,
                    Role = ERole.User,
                    Status = EUserStatus.IsVerifying,
                    AvatarPath = "image.jpg"
                };

                try
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        Message = e.InnerException
                    });
                }
                _loginInfo = new LoginInfo()
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    IsMailConfirmed = false,
                    Message = MessageMail.Success,
                    UserId = user.Id,
                    UserName = user.UserName
                };
            }

            if (_loginInfo.Message == MessageMail.UserAlreadyCreated)
            {
                return BadRequest(new
                {
                    Message = _loginInfo.Message
                });
            }

            if (_loginInfo.Message == MessageMail.VerifyMail)
            {
                MailClass mailClass = this.GetMailObject(_loginInfo);
                await _mailService.SendMail(mailClass);

                return BadRequest(new
                {
                    Message = MessageMail.VerifyMail
                });
            }

            var message = "";
            if (_loginInfo.Message == MessageMail.Success)
            {
                MailClass mailClass = this.GetMailObject(_loginInfo);
                message = await _mailService.SendMail(mailClass);
            }

            if (message != MessageMail.MailSent)
                return BadRequest(new
                {
                    Message = message
                });
            else
            {
                return Ok(new
                {
                    Message = MessageMail.UserCreatedVerifyMail
                });
            }
        }

        // PUT: api/Users/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, AppUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostUser(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        //Save File
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return _storageService.GetFileUrl(fileName);
        }

        private async Task<LoginInfo> CheckRecordExistence(LoginInfo info)
        {
            LoginInfo loginInfo = null;
            if (!string.IsNullOrEmpty(info.UserName) || !string.IsNullOrEmpty(info.UserId.ToString()))
            {
                loginInfo = new LoginInfo();
                loginInfo = await this.GetLoginUser(info);

                if (loginInfo != null)
                {
                    if (!loginInfo.IsMailConfirmed)
                    {
                        loginInfo.Message = MessageMail.VerifyMail;
                    }
                    else
                    {
                        loginInfo.Message = MessageMail.UserAlreadyCreated;
                    }
                }
            }

            return loginInfo;
        }

        private async Task<LoginInfo> GetLoginUser(LoginInfo info)
        {
            var user = await _context.Users.Where(x => x.UserName == info.UserName || x.Id == info.UserId).FirstOrDefaultAsync();

            if (user == null)
                return null;

            var loginInfo = new LoginInfo()
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName
            };

            loginInfo.IsMailConfirmed = true;

            if (user.Status == EUserStatus.IsVerifying)
                loginInfo.IsMailConfirmed = false;

            return loginInfo;
        }

        private MailClass GetMailObject(LoginInfo userInfo)
        {
            MailClass mailClass = new MailClass();
            mailClass.Subject = "Mail Confirmation";
            mailClass.Body = _mailService.GetMailBody(userInfo);
            mailClass.ToMails = new List<string>()
            {
                userInfo.Email
            };

            return mailClass;
        }

        private MailClass GetMailForgotPasswordObject(LoginInfo userInfo)
        {
            MailClass mailClass = new MailClass();
            mailClass.Subject = "Mail Confirmation";
            mailClass.Body = _mailService.GetMailBodyToForgotPassword(userInfo);
            mailClass.ToMails = new List<string>()
            {
                userInfo.Email
            };

            return mailClass;
        }

        private string GenerateJSONWebToken(AppUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, userInfo.FullName),
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userInfo.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }

        private LoginInfo GetDataFromToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();

            LoginInfo info = new LoginInfo()
            {
                UserId = new Guid(claims[0].Value),
                FullName = claims[1].Value,
                UserName = claims[2].Value,
                Email = claims[3].Value
            };
            return info;
        }
    }
}