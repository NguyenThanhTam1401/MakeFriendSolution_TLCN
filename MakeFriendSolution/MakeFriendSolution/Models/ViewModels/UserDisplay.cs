using MakeFriendSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class UserDisplay
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AvatarPath { get; set; }
        public bool HasAvatar { get; set; }
        public string Summary { get; set; }
        public int NumberOfFollowers { get; set; } = 0;
        public bool Followed { get; set; } = false;

        public int NumberOfFavoritors { get; set; } = 0;
        public bool Favorited { get; set; } = false;
        public int NumberOfImages { get; set; }
        public double Point { get; set; } = 0;
        private IStorageService _storageService { get; set; }

        public UserDisplay()
        {
        }

        public UserDisplay(AppUser user, IStorageService storageService)
        {
            this._storageService = storageService;
            Id = user.Id;
            FullName = user.FullName;
            CreatedAt = user.CreatedAt;
            Summary = user.Summary;
            Dob = user.Dob;
            Point = user.Point;
            GetImagePath(user);
        }

        private void GetImagePath(AppUser user)
        {
            try
            {
                byte[] imageBits = System.IO.File.ReadAllBytes($"./{_storageService.GetFileUrl(user.AvatarPath)}");
                this.AvatarPath = Convert.ToBase64String(imageBits);
                this.HasAvatar = true;
            }
            catch
            {
                this.HasAvatar = false;
                this.AvatarPath = user.AvatarPath;
            }
        }
    }
}