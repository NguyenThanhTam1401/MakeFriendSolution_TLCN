using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsAdmin { get; set; }
        public bool Disable { get; set; }
        public string FullName { get; set; }

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