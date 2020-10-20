using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeFriendSolution.EF;
using MakeFriendSolution.HubConfig;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.TimerFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IHubContext<ChartHub> _hub;
        private readonly MakeFriendDbContext _context;

        public ChatsController(IHubContext<ChartHub> hub, MakeFriendDbContext context)
        {
            _hub = hub;
            _context = context;
        }

        public async Task<IActionResult> Post([FromForm] CreateMessageRequest request)
        {
            var result = await SaveMessage(request);
            if (result == null)
            {
                return BadRequest("Can not save message!");
            }

            var response = new
            {
                SenderId = result.SenderId,
                ReceiverId = result.ReceiverId,
                Content = result.Content,
                SentAt = result.SentAt,
                Id = result.Id
            };

            //var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", response));
            await _hub.Clients.All.SendAsync("transferData", response);

            return Ok(new { Message = "Request complete!" });
        }

        [HttpGet("MoreMessages")]
        public async Task<IActionResult> GetMessages([FromQuery] StartChatRequest request)
        {
            var messages = await _context.HaveMessages
                .Where(x => (x.SenderId == request.SenderId && x.ReceiverId == request.ReceiverId) || (x.SenderId == request.ReceiverId && x.ReceiverId == request.SenderId))
                .OrderByDescending(x => x.SentAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();

            List<MessageResponse> responses = new List<MessageResponse>();

            foreach (var item in messages)
            {
                MessageResponse res = new MessageResponse(item);
                responses.Add(res);
            }
            return Ok(responses);
        }

        [HttpGet("friends/{userId}")]
        private async Task<IActionResult> GetFriendList(Guid userId, [FromQuery] PagingRequest pagingRequest)
        {
            var friends = new List<UserDisplay>();

            var user = await _context.Users.Where(x => x.Id == userId)
                .Include(x => x.SendMessages)
                .Include(x => x.ReceiveMessages)
                .FirstOrDefaultAsync();

            foreach (HaveMessage send in user.SendMessages)
            {
                if (!friends.Any(x => x.Id == send.ReceiverId))
                {
                    //var sendUser 
                    //var friend = new
                }
            }
            return Ok();
        }

        private async Task<HaveMessage> SaveMessage(CreateMessageRequest message)
        {
            var newMessage = new HaveMessage()
            {
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                SentAt = DateTime.Now
            };

            try
            {
                _context.HaveMessages.Add(newMessage);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return newMessage;
        }
    }
}