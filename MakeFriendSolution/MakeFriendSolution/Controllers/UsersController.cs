using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Authorization;
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
        private ISessionService _sessionService;
        private readonly IUserApplication _userApplication;

        public UsersController(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService, IUserApplication userApplication)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
            _userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpGet("newUsers")]
        public async Task<IActionResult> GetNewestUsers([FromQuery] PagingRequest request)
        {
            var users = await _userApplication.GetActiveUsers(Guid.Empty, false);

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
            var users = await _userApplication.GetActiveUsers(Guid.Empty, false);

            var loginInfo = _sessionService.GetDataFromToken();
            if (loginInfo != null)
            {
                users = users.Where(x => x.Id != loginInfo.UserId).ToList();
            }
            //Get user display
            var userDisplays = await _userApplication.GetUserDisplay(users, true);

            var response = userDisplays
                .OrderByDescending(x => x.NumberOfFavoritors)
                .ThenBy(x => x.FullName)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();

            foreach (UserDisplay item in response)
            {
                item.GetImagePath();
                if (loginInfo != null)
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

            var user = await _userApplication.GetUserByEmail(request.Email);

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

            //if (oldAvatar != "image.png")
            //{
            //    await _storageService.DeleteFileAsync(oldAvatar);
            //}

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

            var favorited = await _userApplication.GetFavoriteById(sessionUser.UserId, userId);

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

        [Authorize(Roles = "Admin")]
        [HttpGet("filterUsers")]
        public async Task<IActionResult> FilterUsers([FromQuery] FilterUserRequest request)
        {
            var users = new List<AppUser>();

            switch (request.Feature)
            {
                case "FullName":
                    users = request.IsAscending ? await _context.Users.OrderBy(x => x.FullName).ToListAsync() : await _context.Users.OrderByDescending(x => x.FullName).ToListAsync();

                    break;

                case "Like":
                    users = request.IsAscending ? await _context.Users.OrderBy(x => x.NumberOfLikes).ToListAsync() : await _context.Users.OrderByDescending(x => x.NumberOfLikes).ToListAsync();
                    break;

                case "Follow":
                    users = request.IsAscending ? await _context.Users.OrderBy(x => x.NumberOfFiends).ToListAsync() : await _context.Users.OrderByDescending(x => x.NumberOfFiends).ToListAsync();

                    break;

                case "ImageCount":
                    users = request.IsAscending ? await _context.Users.OrderBy(x => x.NumberOfImages).ToListAsync() : await _context.Users.OrderByDescending(x => x.NumberOfImages).ToListAsync();

                    break;

                case "Status":
                    users = request.IsAscending ? await _context.Users.OrderBy(x => x.Status).ToListAsync() : await _context.Users.OrderByDescending(x => x.Status).ToListAsync();

                    break;

                default:
                    break;
            }

            var pageTotal = users.Count / request.PageSize;

            users = users
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize).ToList();

            var response = await _userApplication.GetUserDisplay(users);

            return Ok(new
            {
                data = response,
                pageTotal = pageTotal
            });
        }

        [AllowAnonymous]
        [HttpPost("createDemoUser")]
        public async Task<IActionResult> CreateDemoUser([FromForm] SignUpSystemRequest request)
        {
            var user = new AppUser()
            {
                Email = request.Email,
                FullName = request.FullName,
                PassWord = request.Password,
                IsInfoUpdated = false,
                TypeAccount = ETypeAccount.System,
                AvatarPath = "image.png",
                Status = EUserStatus.Active,
                UserName = request.Email,
                Role = ERole.User,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("block/{userId}")]
        public async Task<IActionResult> BlockUser(Guid userId)
        {
            var message = await _userApplication.DisableUser(userId);
            return Ok(new
            {
                Message = message
            });
        }

        [Authorize]
        [HttpGet("blackList")]
        public async Task<IActionResult> GetBlackList()
        {
            var userInfo = _sessionService.GetDataFromToken();
            if (userInfo == null)
            {
                return NotFound(new
                {
                    Message = "Can not found user"
                });
            }

            var blocks = await _context.BlockUsers.Where(x => x.FromUserId == userInfo.UserId && x.IsLock).ToListAsync();
            var userDisplays = new List<UserDisplay>();

            foreach (var item in blocks)
            {
                var user = new UserDisplay(await _userApplication.GetById(item.ToUserId), _storageService);
                userDisplays.Add(user);
            }

            return Ok(userDisplays);
        }

        [Authorize]
        [HttpPost("blackList/{userid}")]
        public async Task<IActionResult> AddToBlackList(Guid userId)
        {
            if (!await _userApplication.IsExist(userId))
                return NotFound(new
                {
                    Message = "Not found User"
                });

            var userInfo = _sessionService.GetDataFromToken();

            var block = await _context.BlockUsers
                .Where(x => x.FromUserId == userInfo.UserId && x.ToUserId == userId)
                .FirstOrDefaultAsync();

            if (block == null)
            {
                block = new BlockUser()
                {
                    FromUserId = userInfo.UserId,
                    ToUserId = userId,
                    IsLock = true
                };

                _context.BlockUsers.Add(block);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Locked"
                });
            }

            var Message = "Unlocked";

            if (!block.IsLock)
            {
                Message = "Locked";
            }

            block.IsLock = !block.IsLock;

            _context.BlockUsers.Update(block);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = Message
            });
        }
    }
}