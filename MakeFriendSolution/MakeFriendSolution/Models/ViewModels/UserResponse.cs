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
            Role = user.Role.ToString();
            FullName = user.FullName;
            Gender = user.Gender.ToString();
            Location = user.Location.ToString();
            Status = user.Status.ToString();
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Job = user.Job.ToString();
            Profile = new ProfileResponse(user);
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string AvatarPath { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Point { get; set; }
        public bool HasAvatar { get; set; }
        public string Job { get; set; }
        public string Token { get; set; }
        public ProfileResponse Profile { get; set; }
    }
}