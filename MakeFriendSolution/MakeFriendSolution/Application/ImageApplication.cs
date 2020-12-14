using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Application
{
    public class ImageApplication : IImageApplication
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private readonly ISessionService _sessionService;

        public ImageApplication(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
        }

        public async Task<ThumbnailImage> GetImageById(int imageId)
        {
            return await _context.ThumbnailImages.FindAsync(imageId);
        }

        public async Task<List<ImageResponse>> GetImageByUserId(Guid userId)
        {
            var images = await _context.ThumbnailImages.Where(x => x.UserId == userId).ToListAsync();
            var response = new List<ImageResponse>();
            foreach (var image in images)
            {
                var imageRes = new ImageResponse(image, _storageService);
                response.Add(imageRes);
            }

            return response;
        }

        public async Task<bool> IsExist(int imageId)
        {
            return await _context.ThumbnailImages.AnyAsync(x => x.Id == imageId);
        }

        public async Task<string> LikeImage(LikeImageRequest request)
        {
            var isLike = await _context.LikeImages
            .AnyAsync(x => x.UserId == request.UserId && x.ImageId == request.ImageId);

            var message = "";

            if (isLike)
            {
                await this.Unlike(request);
                message = "Liked";
            }
            else
            {
                await this.Like(request);
                message = "Unliked";
            }

            return message;
        }

        private async Task<bool> Unlike(LikeImageRequest request)
        {
            var likeImage = await _context.LikeImages.Where(x => x.UserId == request.UserId && x.ImageId == request.ImageId).FirstOrDefaultAsync();
            var image = await _context.ThumbnailImages.FindAsync(request.ImageId);
            image.NumberOflikes--;
            
            try
            {
                _context.LikeImages.Remove(likeImage);
                if(image.NumberOflikes < 0)
                {
                    image.NumberOflikes = 0;
                }
                _context.ThumbnailImages.Update(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
        private async Task<bool> Like(LikeImageRequest request)
        {
            var likeImage = new LikeImage()
            {
                UserId = request.UserId,
                ImageId = request.ImageId
            };
            var image = await _context.ThumbnailImages.FindAsync(request.ImageId);
            image.NumberOflikes++;
            try
            {
                _context.ThumbnailImages.Update(image);
                _context.LikeImages.Add(likeImage);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
