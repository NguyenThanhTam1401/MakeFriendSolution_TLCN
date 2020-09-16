using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }

        public ImageResponse()
        {
        }

        public ImageResponse(ThumbnailImage image)
        {
            Id = image.Id;
            UserId = image.UserId;
            Title = image.Title;
            ImagePath = image.ImagePath;
            CreatedAt = image.CreatedAt;
        }
    }
}