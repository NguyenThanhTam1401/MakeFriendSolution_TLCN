using MakeFriendSolution.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class UserResponse
    {
        public UserResponse(AppUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            PassWord = user.PassWord;
            Role = user.Role.ToString();
            FullName = user.FullName;
            Gender = user.Gender.ToString();
            AvatarPath = user.AvatarPath;
            Location = user.Location.ToString();
            Status = user.Status.ToString();
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string AvatarPath { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Point { get; set; }
        public ProfileResponse Profile { get; set; }
    }
}