using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Common;
using System.Net.Http.Headers;
using System.IO;
using MakeFriendSolution.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/Users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("pagingUsers")]
        public async Task<IActionResult> HomeDisplayUser([FromQuery] PagingRequest request)
        {
            var users = await _context.Users
                .Where(x => x.Status == Models.Enum.EUserStatus.Active)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();

            var userDisplays = new List<UserDisplay>();

            foreach (var user in users)
            {
                var userDisplay = new UserDisplay(user, this._storageService);

                var followResult = await this.GetNumberOfFollowers(userDisplay.Id, false, Guid.Empty);
                userDisplay.NumberOfFollowers = followResult.Item1;
                userDisplay.Followed = followResult.Item2;

                userDisplays.Add(userDisplay);
            }

            return Ok(userDisplays);
        }

        private async Task<(int, bool)> GetNumberOfFollowers(Guid userId, bool isLogin, Guid currentUserId)
        {
            var numberOfFollowers = await _context.Follows.Where(x => x.ToUserId == userId).CountAsync();
            bool followed = false;
            if (isLogin)
            {
                followed = await _context.Follows.AnyAsync(x => x.FromUserId == currentUserId && x.ToUserId == userId);
            }

            return (numberOfFollowers, followed);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.ConfirmPassword) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest(new
                {
                    Message = "Vui lòng điền đầy đủ thông tin"
                });
            }

            var user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new
                {
                    Message = "Can not find user with email = " + request.Email
                });
            }

            if (user.Status != Models.Enum.EUserStatus.Active)
            {
                return BadRequest(new
                {
                    Message = "Account is not active"
                });
            }

            if (user.PassWord != request.OldPassword.Trim())
            {
                return BadRequest(new
                {
                    Message = "Password is not correct"
                });
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest(new
                {
                    Message = "Confirm password is not correct"
                });
            }

            try
            {
                user.PassWord = request.NewPassword;
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
                Message = "Update password success"
            });
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Can not find user by id = " + userId);
            var respone = new UserResponse(user, _storageService);

            var data = this.GetDataFromToken();
            return Ok(data);
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