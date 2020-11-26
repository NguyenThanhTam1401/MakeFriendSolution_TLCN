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
using MakeFriendSolution.Models.Enum;
using MakeFriendSolution.Application;

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
        private readonly IUserApplication _userApplication;
        public UsersController(MakeFriendDbContext context, IStorageService storageService, IMailService mailService, IConfiguration config, ISessionService sessionService, IUserApplication userApplication)
        {
            _context = context;
            _storageService = storageService;
            _mailService = mailService;
            _config = config;
            _sessionService = sessionService;
            _userApplication = userApplication;
        }

        // GET: api/Users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("newUsers")]
        public async Task<IActionResult> GetNewestUsers([FromQuery] PagingRequest request)
        {
            var users = await _context.Users
                .Where(x => x.Status == Models.Enum.EUserStatus.Active && x.IsInfoUpdated)
                .ToListAsync();

            var loginInfo = _sessionService.GetDataFromToken();
            if (loginInfo != null)
            {
                users = users.Where(x => x.Id != loginInfo.UserId).ToList();
            }

            users = users.OrderByDescending(x => x.CreatedAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();

            var userDisplays = await _userApplication.GetUserDisplay(users);

            return Ok(userDisplays);
        }

        [AllowAnonymous]
        [HttpGet("favoritest")]
        public async Task<IActionResult> GetFavoritestUsers([FromQuery] PagingRequest request)
        {
            var users = await _context.Users
                .Where(x => x.Status == Models.Enum.EUserStatus.Active && x.IsInfoUpdated)
                .ToListAsync();

            var loginInfo = _sessionService.GetDataFromToken();
            if (loginInfo != null)
            {
                users = users.Where(x => x.Id != loginInfo.UserId).ToList();
            }
            //Get user display
            var userDisplays = await _userApplication.GetUserDisplay(users, true);

            var response = userDisplays
                .OrderByDescending(x => x.NumberOfFavoritors)
                .ThenBy(x=>x.FullName)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();

            foreach (UserDisplay item in response)
            {
                item.GetImagePath();
                if(loginInfo != null)
                {
                    item.Followed = await _userApplication.IsFollowed(item.Id, loginInfo.UserId);
                    item.Favorited = await _userApplication.IsLiked(item.Id, loginInfo.UserId);
                }
            }

            var pageTotal = users.Count / request.PageSize;
            return Ok(new
            {
                data = response,
                pageTotal = pageTotal
            });
        }

        [AllowAnonymous]
        [HttpGet("similar")]
        public async Task<IActionResult> GetMatrix(Guid userId, [FromQuery] FilterUserViewModel filter)
        {
            var usersResponse = new List<UserResponse>();

            var user = await _userApplication.GetById(userId);

            var users = await _context.Users
                .Where(x => x.Id != userId && x.Gender == user.FindPeople)
                .ToListAsync();

            //FilterUsers
            _userApplication.FilterUers(ref users, filter);

            users.Insert(0, user);

            foreach (var item in users)
            {
                UserResponse userResponse = new UserResponse(item, _storageService);
                usersResponse.Add(userResponse);
            }

            int sl = users.Count;

            double[,] usersMatrix = new double[sl, 12];
            for (int i = 0; i < sl; i++)
            {
                usersMatrix[i, 0] = (double)users[i].Marriage;
                usersMatrix[i, 1] = (double)users[i].Target;
                usersMatrix[i, 2] = (double)users[i].Education;
                usersMatrix[i, 3] = (double)users[i].Body;
                usersMatrix[i, 4] = (double)users[i].Religion;
                usersMatrix[i, 5] = (double)users[i].Smoking;
                usersMatrix[i, 6] = (double)users[i].DrinkBeer;
                usersMatrix[i, 7] = (double)users[i].FavoriteMovie;
                usersMatrix[i, 8] = (double)users[i].AtmosphereLike;
                usersMatrix[i, 9] = (double)users[i].Character;
                usersMatrix[i, 10] = (double)users[i].LifeStyle;
                usersMatrix[i, 11] = (double)users[i].MostValuable;
            }

            cMatrix m = new cMatrix();
            m.Row = sl;
            m.Column = 12;
            m.Matrix = usersMatrix;

            List<double> kq = new List<double>();
            kq = m.SimilarityCalculate();

            for (int i = 0; i < kq.Count; i++)
            {
                usersResponse[i].Point = kq[i];
            }

            //usersResponse.RemoveAt(0);
            usersResponse = usersResponse.OrderByDescending(o => o.Point).ToList();
            return Ok(usersResponse);
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
                user = await _userApplication.UpdateUser(user);
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
        [HttpPut("avatar")]
        public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarRequest request)
        {
            var user = await _userApplication.GetById(request.UserId);
            if (user.Status != EUserStatus.Active)
            {
                return BadRequest(new
                {
                    Message = "Account is not active!"
                });
            }
            var oldAvatar = user.AvatarPath;

            if (request.Avatar == null)
            {
                return BadRequest(new
                {
                    Message = "File is required!"
                });
            }

            user.AvatarPath = await _userApplication.SaveFile(request.Avatar);

            if (oldAvatar != "image.png")
            {
                await _storageService.DeleteFileAsync(oldAvatar);
            }

            user = await _userApplication.UpdateUser(user);

            var response = new UserResponse(user, _storageService);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _userApplication.GetById(userId);
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
            respone.Followed = await _userApplication.IsFollowed(userId, sessionUser.UserId);
            respone.Favorited = await _userApplication.IsLiked(userId, sessionUser.UserId);

            respone.Blocked = await _userApplication.GetBlockStatus(sessionUser.UserId, userId);

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

            var user = await _userApplication.GetById(sessionUser.UserId);

            var message = "";

            if (followed == null)
            {
                var follow = new Follow()
                {
                    Content = "",
                    FromUserId = sessionUser.UserId,
                    ToUserId = userId
                };
                user.NumberOfFiends++;

                user = await _userApplication.UpdateUser(user);
                _context.Follows.Add(follow);
                message = "Followed";
            }
            else
            {
                user.NumberOfFiends--;

                user = await _userApplication.UpdateUser(user);
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

            var user = await _userApplication.GetById(userId);

            var message = "";

            if (favorited == null)
            {
                var favorite = new Favorite()
                {
                    Content = "",
                    FromUserId = sessionUser.UserId,
                    ToUserId = userId
                };
                user.NumberOfLikes++;

                user = await _userApplication.UpdateUser(user);
                _context.Favorites.Add(favorite);
                message = "Favorited";
            }
            else
            {
                user.NumberOfLikes--;

                user = await _userApplication.UpdateUser(user);
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
        public async Task<IActionResult> GetFriends(Guid userId)
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
                item.Followed = await _userApplication.IsFollowed(item.Id, sessionUser.UserId);

                item.Favorited = await _userApplication.IsLiked(item.Id, sessionUser.UserId);
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

            var favoritors = await _context
                .Favorites
                .Where(x => x.FromUserId == userId)
                .Include(x => x.ToUser)
                .ToListAsync();

            var response = favoritors.Select(x => new UserDisplay(x.ToUser, _storageService));
            foreach (var item in response)
            {
                item.Followed = await _userApplication.IsFollowed(item.Id, sessionUser.UserId);
                item.Favorited = await _userApplication.IsLiked(item.Id, sessionUser.UserId);
            }

            return Ok(response);
        }

    }
}