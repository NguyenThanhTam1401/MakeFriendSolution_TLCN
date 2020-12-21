using MakeFriendSolution.Models;
using MakeFriendSolution.Models.Enum;
using MakeFriendSolution.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeFriendSolution.Application
{
    public interface IUserApplication
    {
        Task<AppUser> GetById(Guid id);
        Task<AppUser> UpdateUser(AppUser user);

        Task<string> SaveFile(IFormFile file);
        Task<bool> IsLiked(Guid userId, Guid currentUserId);
        Task<bool> IsFollowed(Guid userId, Guid currentUserId);
        Task<bool> GetBlockStatus(Guid currentUserId, Guid toUserId);
        int CalculateAge(DateTime birthDay);
        void FilterUers(ref List<AppUser> users, FilterUserViewModel filter);
        Task<List<UserDisplay>> GetUserDisplay(List<AppUser> users, bool nonImage = false);
        EAgeGroup GetAgeGroup(DateTime birthDay);
        Task<AppUser> BidingUserRequest(AppUser user, UserRequest request);
        Task<bool> IsExist(Guid userId);
        Task<List<AppUser>> GetActiveUsers();
        Task<AppUser> GetUserByEmail(string email);
        Task<Favorite> GetFavoriteById(Guid fromId, Guid toId);
    }
}
