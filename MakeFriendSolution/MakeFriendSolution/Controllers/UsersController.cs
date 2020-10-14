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
using System.Runtime.CompilerServices;
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
        private ISessionService _sessionService;

        public UsersController(MakeFriendDbContext context, IStorageService storageService, IMailService mailService, IConfiguration config, ISessionService sessionService)
        {
            _context = context;
            _storageService = storageService;
            _mailService = mailService;
            _config = config;
            _sessionService = sessionService;
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

            //Get Session user - Check login
            var isLogin = true;
            var sessionUser = _sessionService.GetDataFromToken();
            if (sessionUser == null)
            {
                isLogin = false;
                sessionUser = new LoginInfo()
                {
                    UserId = Guid.NewGuid()
                };
            }

            foreach (var user in users)
            {
                var userDisplay = new UserDisplay(user, this._storageService);

                var followResult = await this.GetNumberOfFollowers(userDisplay.Id, isLogin, sessionUser.UserId);
                userDisplay.NumberOfFollowers = followResult.Item1;
                userDisplay.Followed = followResult.Item2;

                var favoriteResult = await this.GetNumberOfFavoritors(userDisplay.Id, isLogin, sessionUser.UserId);
                userDisplay.NumberOfFavoritors = favoriteResult.Item1;
                userDisplay.Favorited = favoriteResult.Item2;

                userDisplays.Add(userDisplay);
            }

            return Ok(userDisplays);
        }

        [Authorize]
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
            var sessionUser = _sessionService.GetDataFromToken();
            if (sessionUser == null)
            {
                return BadRequest(new
                {
                    Message = "Can not read session"
                });
            }
            //Get Follow & Favorite
            var follow = await this.GetNumberOfFollowers(userId, true, sessionUser.UserId);

            respone.NumberOfFollowers = follow.Item1;
            respone.Followed = follow.Item2;

            var favorite = await this.GetNumberOfFavoritors(userId, true, sessionUser.UserId);

            respone.NumberOfFollowers = follow.Item1;
            respone.Followed = follow.Item2;

            respone.NumberOfFavoriting = favorite.Item1;
            respone.Favorited = favorite.Item2;

            return Ok(respone);
        }

        [Authorize]
        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromForm] Guid userId)
        {
            var sessionUser = _sessionService.GetDataFromToken();

            if (sessionUser == null)
            {
                return BadRequest(new
                {
                    Message = "Can not read token"
                });
            }

            if (sessionUser.UserId == userId)
            {
                return BadRequest(new
                {
                    Message = "Can not follow yourself"
                });
            }

            var followed = await _context.Follows
                .Where(x => x.FromUserId == sessionUser.UserId && x.ToUserId == userId)
                .FirstOrDefaultAsync();

            var message = "";

            if (followed == null)
            {
                var follow = new Follow()
                {
                    Content = "",
                    FromUserId = sessionUser.UserId,
                    ToUserId = userId
                };

                _context.Follows.Add(follow);
                message = "Followed";
            }
            else
            {
                _context.Follows.Remove(followed);
                message = "Unfollowed";
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(501, new
                {
                    Message = e.Message
                });
            }
            return Ok(new
            {
                Message = message
            });
        }

        [Authorize]
        [HttpPost("favorite")]
        public async Task<IActionResult> Favorite([FromForm] Guid userId)
        {
            var sessionUser = _sessionService.GetDataFromToken();

            if (sessionUser == null)
            {
                return BadRequest(new
                {
                    Message = "Can not read session"
                });
            }

            if (sessionUser.UserId == userId)
            {
                return BadRequest(new
                {
                    Message = "Can not favorite yourself"
                });
            }

            var favorited = await _context.Favorites
                .Where(x => x.FromUserId == sessionUser.UserId && x.ToUserId == userId)
                .FirstOrDefaultAsync();

            var message = "";

            if (favorited == null)
            {
                var favorite = new Favorite()
                {
                    Content = "",
                    FromUserId = sessionUser.UserId,
                    ToUserId = userId
                };

                _context.Favorites.Add(favorite);
                message = "Favorited";
            }
            else
            {
                _context.Favorites.Remove(favorited);
                message = "Unfavorited";
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(501, new
                {
                    Message = e.Message
                });
            }
            return Ok(new
            {
                Message = message
            });
        }

        [Authorize]
        [HttpGet("follow/{userId}")]
        public async Task<IActionResult> GetFollowers(Guid userId)
        {
            var sessionUser = _sessionService.GetDataFromToken();
            if (sessionUser == null)
            {
                return BadRequest(new
                {
                    Message = "Can not read session"
                });
            }

            if (userId != sessionUser.UserId)
            {
                return StatusCode(401);
            }

            var followers = await _context.Follows.Where(x => x.FromUserId == userId).Include(x => x.ToUser).ToListAsync();
            var response = followers.Select(x => new UserDisplay(x.ToUser, _storageService));
            foreach (var item in response)
            {
                var follow = await this.GetNumberOfFollowers(item.Id, true, sessionUser.UserId);
                item.NumberOfFollowers = follow.Item1;
                item.Followed = follow.Item2;

                var favorite = await this.GetNumberOfFavoritors(item.Id, true, sessionUser.UserId);
                item.NumberOfFavoritors = favorite.Item1;
                item.Favorited = favorite.Item2;
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("favorite/{userId}")]
        public async Task<IActionResult> GetFavoritors(Guid userId)
        {
            var sessionUser = _sessionService.GetDataFromToken();
            if (sessionUser == null)
            {
                return BadRequest(new
                {
                    Message = "Can not read session"
                });
            }

            if (userId != sessionUser.UserId)
            {
                return StatusCode(401);
            }

            var favoritors = await _context.Favorites.Where(x => x.FromUserId == userId).Include(x => x.ToUser).ToListAsync();
            var response = favoritors.Select(x => new UserDisplay(x.ToUser, _storageService));
            foreach (var item in response)
            {
                var follow = await this.GetNumberOfFollowers(item.Id, true, sessionUser.UserId);
                item.NumberOfFollowers = follow.Item1;
                item.Followed = follow.Item2;

                var favorite = await this.GetNumberOfFavoritors(item.Id, true, sessionUser.UserId);
                item.NumberOfFavoritors = favorite.Item1;
                item.Favorited = favorite.Item2;
            }

            return Ok(response);
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

        private async Task<(int, bool)> GetNumberOfFavoritors(Guid userId, bool isLogin, Guid currentUserId)
        {
            var numberOfFavoritors = await _context.Favorites.Where(x => x.ToUserId == userId).CountAsync();
            bool favorited = false;
            if (isLogin)
            {
                favorited = await _context.Favorites.AnyAsync(x => x.FromUserId == currentUserId && x.ToUserId == userId);
            }

            return (numberOfFavoritors, favorited);
        }
    }
}