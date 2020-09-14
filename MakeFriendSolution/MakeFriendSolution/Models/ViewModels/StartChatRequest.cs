using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class StartChatRequest: PagingRequest
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}