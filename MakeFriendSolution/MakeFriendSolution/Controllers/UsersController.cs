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

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;

        public UsersController(MakeFriendDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            Enum.TryParse("Mảnh_Mai", out EBody body);
            var obj = new
            {
                body = body.ToString()
            };
            return Ok(obj);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var user = await _context.Users
                .Where(x => x.UserName == request.UserName.Trim()).FirstOrDefaultAsync();

            if (user == null)
            {
                var message = "Can not find User with UserName is " + request.UserName;
                return BadRequest(message);
            }
            if (user.PassWord != request.Password.Trim())
            {
                var message = "Password is not correct!";
                return BadRequest(message);
            }

            var userResponse = new UserResponse(user);

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

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var user = new AppUser()
            {
                CreatedAt = DateTime.Now,
                Email = request.Email,
                FullName = request.FullName,
                Gender = request.Gender,
                PassWord = request.Password,
                PhoneNumber = request.PhoneNumber,
                Role = ERole.User,
                Status = EUserStatus.Active,
                UserName = request.UserName
            };
            //Save Image
            if (request.AvartarFile != null)
            {
                user.AvatarPath = await this.SaveFile(request.AvartarFile);
            }
            else
            {
                user.AvatarPath = "image.jpg";
            }

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var message = e.InnerException;
                return BadRequest(message);
            }
            byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(user.AvatarPath)}");
            user.AvatarPath = Convert.ToBase64String(imageBits);
            return Ok(user);
        }

        // PUT: api/Users/5
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
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostUser(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
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
    }
}