using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models
{
    public class HaveMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}