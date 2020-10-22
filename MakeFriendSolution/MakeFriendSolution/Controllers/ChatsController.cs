using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeFriendSolution.EF;
using MakeFriendSolution.HubConfig;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
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
        private readonly IStorageService _storageService;

        public ChatsController(IHubContext<ChartHub> hub, MakeFriendDbContext context, IStorageService storageService)
        {
            _hub = hub;
            _context = context;
            _storageService = storageService;
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
        public async Task<IActionResult> GetFriendList(Guid userId, [FromQuery] PagingRequest request)
        {
            var messages = new List<HaveMessage>();

            var recentMessages = await _context.HaveMessages
                .Where(x => x.SenderId == userId || x.ReceiverId == userId)
                .OrderByDescending(x => x.SentAt)
                .ToListAsync();

            foreach (HaveMessage message in recentMessages)
            {
                if (!messages.Any(x => x.ReceiverId == message.ReceiverId || x.SenderId == message.SenderId || x.ReceiverId == message.SenderId || x.SenderId == message.ReceiverId))
                {
                    messages.Add(message);
                }
            }
            messages = messages
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
            var friendList = new List<FriendResponse>();
            foreach (HaveMessage message in messages)
            {
                AppUser user = new AppUser();
                if (message.SenderId == userId)
                {
                    user = await _context.Users.FindAsync(message.ReceiverId);
                }
                else
                {
                    user = await _context.Users.FindAsync(message.SenderId);
                }

                var userDisplay = new UserDisplay(user, _storageService);
                var messageResponses = new List<MessageResponse>();
                var messageResponse = new MessageResponse(message);
                messageResponses.Add(messageResponse);

                var friend = new FriendResponse()
                {
                    Messages = messageResponses,
                    User = userDisplay
                };

                friendList.Add(friend);
            }

            return Ok(friendList);
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