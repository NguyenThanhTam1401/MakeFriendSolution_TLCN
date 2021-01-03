using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MakeFriendSolution.Application;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakeFriendSolution.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private readonly ISessionService _sessionService;
        private readonly IImageApplication _imageApplication;
        private readonly IUserApplication _userApplication;
        public ImagesController(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService, IImageApplication imageApplication, IUserApplication userApplication)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
            _imageApplication = imageApplication;
            _userApplication = userApplication;
        }


        /// <summary>
        /// Lấy thông tin user bằng ID
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ImageResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetImageByUserId(Guid userId)
        {
            var isExist = await _context.Users.AnyAsync(x => x.Id == userId);
            if (!isExist)
            {
                return NotFound(new
                {
                    Message = "Can not find User with id = " + userId
                });
            }
            var images = await _context.ThumbnailImages.Where(x => x.UserId == userId).ToListAsync();
            var errorImages = new List<ThumbnailImage>();
            var response = new List<ImageResponse>();
            foreach (var item in images)
            {
                try
                {
                    var imageResponse = new ImageResponse(item, _storageService);
                    response.Add(imageResponse);
                }
                catch
                {
                    errorImages.Add(item);
                }
            }

            var userInfo = _sessionService.GetDataFromToken();
            if (userInfo != null)
            {
                foreach (var item in response)
                {
                    item.liked = await this.IsLiked(userInfo.UserId, item.Id);
                }
            }

            if (errorImages.Count > 0)
            {
                try
                {
                    _context.RemoveRange(errorImages);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return StatusCode(500, new
                    {
                        Message = e.InnerException
                    });
                }
            }

            return Ok(response);
        }


        /// <summary>
        /// Get image by Id
        /// </summary>
        /// <param name="imageId">Id hình ảnh</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetById(int imageId)
        {
            var image = await _context.ThumbnailImages.FindAsync(imageId);

            if (image == null)
            {
                return NotFound(new
                {
                    Message = "Can not find image with id = " + imageId
                });
            }

            var imageResponse = new ImageResponse(image, _storageService);

            return Ok(imageResponse);
        }


        /// <summary>
        /// Thêm hình ảnh
        /// </summary>
        /// <param name="request">Files hình ảnh, userId và content</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<ImageResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Create([FromForm] ImageRequest request)
        {
            var userInfo = _sessionService.GetDataFromToken();
            if (userInfo.UserId != request.UserId)
            {
                return StatusCode(401, new
                {
                    Message = "This userId is not yours"
                });
            }

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "Can not find user with id = " + request.UserId
                });
            }
            
            var newImages = new List<ThumbnailImage>();
            for (int i = 0; i < request.Images.Count; i++)
            {
                var image = new ThumbnailImage();
                image.CreatedAt = DateTime.Now;
                image.Title = request.Title;
                image.UserId = request.UserId;

                if (request.Images[i] != null)
                {
                    image.ImagePath = await _storageService.SaveFile(request.Images[i]);
                }

                newImages.Add(image);
            }

            user.NumberOfImages += newImages.Count;
            var imagesResponse = new List<ImageResponse>();
            try
            {
                _context.Users.Update(user);
                imagesResponse = await _imageApplication.CreateImages(newImages);
            }
            catch (Exception e)
            {
                return StatusCode(501, new
                {
                    Message = e.InnerException
                });
            }

            return Ok(imagesResponse);
        }


        /// <summary>
        /// Xóa hình ảnh theo ID
        /// </summary>
        /// <param name="imageId">Id hình ảnh cần xóa</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ImageResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var image = await _context.ThumbnailImages.FindAsync(imageId);

            if (image == null)
            {
                return NotFound(new
                {
                    Message = "Can not find image with id = " + imageId
                });
            }
            var user = await _context.Users.FindAsync(image.UserId);
            user.NumberOfImages--;
            try
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
                _context.Users.Update(user);
                _context.ThumbnailImages.Remove(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(501, new
                {
                    Message = e.InnerException
                });
            }
            return Ok(new
            {
                Message = "Delete success!"
            });
        }


        /// <summary>
        /// Like và unlike ảnh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("like")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> LikeImage([FromForm] LikeImageRequest request)
        {
            var userInfo = _sessionService.GetDataFromToken();
            if (request.UserId != userInfo.UserId)
            {
                return StatusCode(401, new
                {
                    Message = "userId is not correct"
                });
            }

            if (!await _imageApplication.IsExist(request.ImageId))
            {
                return BadRequest(new
                {
                    Message = "Image id is not correct"
                });
            }

            if (!await _userApplication.IsExist(request.UserId))
            {
                return BadRequest(new
                {
                    Message = "User id is not correct"
                });
            }

            var message = await _imageApplication.LikeImage(request);
            
            return Ok(new
            {
                Message = message
            });
        }

        private async Task<bool> IsLiked(Guid userId, int imageId)
        {
            return await _context.LikeImages.AnyAsync(x => x.UserId == userId && x.ImageId == imageId);
        }

    }
}