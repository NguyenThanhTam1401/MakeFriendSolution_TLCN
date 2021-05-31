using MakeFriendSolution.HubConfig.Models;
using MakeFriendSolution.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.HubConfig
{
    public class ChatHub : Hub
    {
		// Use this variable to track user count
		private static int _userCount = 0;
		 
	     // Overridable hub methods  
	     public override async Task OnConnectedAsync()
		{
			_userCount++;
			await Response();

		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			_userCount--;
			await Response();
            //
            await HangUp();

            await base.OnDisconnectedAsync(exception);
        }

		private async Task Response()
        {
			await this.Clients.All.SendAsync("onlineCount", _userCount);
		}

		public string GetConnectionId() => Context.ConnectionId;

        /////////////////////////////////////////////////////////////////

        public RtcIceServer[] GetIceServers()
        {
            // Perhaps Ice server management.

            return new RtcIceServer[] { new RtcIceServer() { Username = "", Credential = "" } };
        }

        public async Task<UserConnection> GetTargetInfo(Guid userId, CallType callType)
        {
            var target = UserConnection.Get(userId);
            var caller = UserConnection.Get(this.Context.ConnectionId);

            if (target != null && caller != null)
            {
                await this.Clients.Client(target.ConnectionId).SendAsync("callerInfo", caller, callType);
            }

            return target;
        }

        public UserConnection GetUserById(Guid userId)
        {
            var user = UserConnection.Get(userId);
            return user;
        }

        public UserConnection GetMyInfo(Guid userId, string connectionId, string userName, string avatarPath)
        {
            var user = UserConnection.Get(userId, connectionId, userName, avatarPath);
            return user;
        }

        public async Task Join(Guid userId, string connectionId, string userName, string avatarPath, string roomName, bool isMobile, CallType callType)
        {
            var user = UserConnection.Get(userId, connectionId, userName, avatarPath, isMobile);
            var room = Room.Get(roomName);

            if (user.CurrentRoom != null)
            {
                room.Users.Remove(user);
            }

            user.CurrentRoom = room;
            room.Users.Add(user);

            await SendUserListUpdate(Clients.Caller, room, true, callType);
            await SendUserListUpdate(Clients.Others, room, false, callType);
        }

        public async Task HangUp()
        {
            var callingUser = UserConnection.Get(Context.ConnectionId);

            if (callingUser == null)
            {
                return;
            }

            if (callingUser.CurrentRoom != null)
            {
                callingUser.CurrentRoom.Users.Remove(callingUser);
                await SendUserListUpdate(Clients.Others, callingUser.CurrentRoom, false);
            }

            var user = callingUser;
            UserConnection.Remove(callingUser);
        }

        // WebRTC Signal Handler
        public async Task SendSignal(string signal, string targetConnectionId, CallType callType)
        {
            var callingUser = UserConnection.Get(Context.ConnectionId);
            var targetUser = UserConnection.Get(targetConnectionId);

            // Make sure both users are valid
            if (callingUser == null || targetUser == null)
            {
                return;
            }

            // These folks are in a call together, let's let em talk WebRTC
            await Clients.Client(targetConnectionId).SendAsync("receiveSignal", callingUser, signal, callType);
        }

        private async Task SendUserListUpdate(IClientProxy to, Room room, bool callTo, CallType callType = CallType.VoiceCall)
        {
            var users = room.Users.Where(x => x.IsCalling).ToList();
            await to.SendAsync(callTo ? "callToUserList" : "updateUserList", room.Name, users, callType);
        }
    }

    public enum CallType
    {
        VideoCall,
        VoiceCall
    }
}