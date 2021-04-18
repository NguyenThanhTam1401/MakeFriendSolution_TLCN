using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MakeFriendSolution.HubConfig.Models
{
    public class UserConnection
    {
        private static readonly List<UserConnection> Users = new List<UserConnection>();
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        [JsonIgnore]
        public Room CurrentRoom { get; set; }

        public static void Remove(UserConnection user)
        {
            Users.Remove(user);
        }

        public static UserConnection Get(string connectionId)
        {
            return Users.Where(u => u.ConnectionId == connectionId).FirstOrDefault();
        }
        public static UserConnection Get(Guid userId)
        {
            return Users.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public static UserConnection Get(Guid userId, string connectionId, string userName )
        {
            lock (Users)
            {
                var current = Users.SingleOrDefault(u => u.ConnectionId == connectionId);

                if (current == default(UserConnection))
                {
                    var duplicate = Users.Where(x => x.UserId == userId).ToList();
                    foreach (var item in duplicate)
                    {
                        Users.Remove(item);
                    }
                    current = new UserConnection
                    {
                        UserId = userId,
                        UserName = userName,
                        ConnectionId = connectionId
                    };
                    Users.Add(current);
                }
                else
                {
                    current.UserId = userId;
                    current.UserName = userName;
                }

                return current;
            }
        }
    }
}
