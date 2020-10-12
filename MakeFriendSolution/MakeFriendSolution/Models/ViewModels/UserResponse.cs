﻿using MakeFriendSolution.Common;
using MakeFriendSolution.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class UserResponse
    {
        private IStorageService _storageService;

        public UserResponse(AppUser user, IStorageService storageService)
        {
            this._storageService = storageService;

            Id = user.Id;
            UserName = user.UserName;
            FullName = user.FullName;
            Gender = user.Gender.ToString();
            Status = user.Status.ToString();
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Summary = user.Summary;
            IsInfoUpdated = user.IsInfoUpdated;

            if (IsInfoUpdated != 0)
            {
                Profile = new ProfileResponse(user);
            }

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

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string AvatarPath { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Point { get; set; }
        public bool HasAvatar { get; set; }
        public string Summary { get; set; }
        public int NumberOfFollowing { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfFavoriting { get; set; }
        public int NumberOfFavoritors { get; set; }
        public bool Followed { get; set; }
        public bool Favorited { get; set; }
        public int IsInfoUpdated { get; set; }
        public string Token { get; set; }
        public ProfileResponse Profile { get; set; }
    }
}