using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MakeFriendSolution.Common;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private readonly ISessionService _sessionService;

        public ImagesController(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
        }

        [HttpGet("all")]
        public async Task<List<ThumbnailImage>> GetAll()
        {
            return await _context.ThumbnailImages.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("user/{userId}")]
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
                    var imageResponse = new ImageResponse(item);
                    byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(item.ImagePath)}");
                    imageResponse.ImagePath = Convert.ToBase64String(imageBits);
                    response.Add(imageResponse);
                }
                catch
                {
                    errorImages.Add(item);
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
                    return StatusCode(501, new
                    {
                        Message = e.InnerException
                    });
                }
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{imageId}")]
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

            var imageResponse = new ImageResponse(image);

            byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(image.ImagePath)}");
            imageResponse.ImagePath = Convert.ToBase64String(imageBits);

            return Ok(imageResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ImageRequest request)
        {
            var sessionUser = _sessionService.GetSessionUser();
            if (sessionUser.UserId != request.UserId)
            {
                return StatusCode(401, new
                {
                    Message = "This userId is not yours"
                });
            }

            var userExist = await _context.Users.AnyAsync(x => x.Id == request.UserId);
            if (!userExist)
            {
                return NotFound(new
                {
                    Message = "Can not find user with id = " + request.UserId
                });
            }
            var image = new ThumbnailImage();
            image.CreatedAt = DateTime.Now;
            image.Title = request.Title;
            image.UserId = request.UserId;

            if (request.Image != null)
            {
                image.ImagePath = await this.SaveFile(request.Image);
            }

            try
            {
                _context.ThumbnailImages.Add(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(501, new
                {
                    Message = e.InnerException
                });
            }

            var response = new ImageResponse(image);
            byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(image.ImagePath)}");
            response.ImagePath = Convert.ToBase64String(imageBits);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{imageId}")]
        public async Task<IActionResult> UpdateById(int imageId, [FromForm] ImageRequest request)
        {
            var image = await _context.ThumbnailImages.FindAsync(imageId);
            if (request.Title != "" && request.Title != null)
            {
                image.Title = request.Title;
            }

            if (request.Image != null)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
                image.ImagePath = await this.SaveFile(request.Image);
            }

            try
            {
                _context.ThumbnailImages.Update(image);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(501, new
                {
                    Message = e.InnerException
                });
            }

            var response = new ImageResponse(image);
            byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(image.ImagePath)}");
            response.ImagePath = Convert.ToBase64String(imageBits);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{imageId}")]
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

            try
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
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

        //Save File
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}