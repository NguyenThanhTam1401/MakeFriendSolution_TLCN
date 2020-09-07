using MakeFriendSolution.Models.Enum;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public ERole Role { get; set; }
        public string FullName { get; set; }
        public EGender Gender { get; set; }
        public string AvatarPath { get; set; }
        public ELocation Location { get; set; }
        public EUserStatus Status { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Profile Profile { get; set; }
        public ICollection<ThumbnailImage> ThumbnailImages { get; set; }
        public ICollection<HaveMessage> SendMessages { get; set; }
        public ICollection<HaveMessage> ReceiveMessages { get; set; }
    }
}