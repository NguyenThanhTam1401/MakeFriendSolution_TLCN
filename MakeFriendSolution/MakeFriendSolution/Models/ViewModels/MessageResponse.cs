using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class MessageResponse
    {
        public MessageResponse()
        {
        }

        public MessageResponse(HaveMessage message)
        {
            Id = message.Id;
            SenderId = message.SenderId;
            ReceiverId = message.ReceiverId;
            Content = message.Content;
            SentAt = message.SentAt;
        }

        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}