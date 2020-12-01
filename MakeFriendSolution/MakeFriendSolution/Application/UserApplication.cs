using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.Enum;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MakeFriendSolution.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private ISessionService _sessionService;

        public UserApplication(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<bool> IsLiked(Guid userId, Guid currentUserId)
        {
            return await _context.Favorites.AnyAsync(x => x.FromUserId == currentUserId && x.ToUserId == userId);
        }

        public async Task<bool> IsFollowed(Guid userId, Guid currentUserId)
        {
            return await _context.Follows.AnyAsync(x => x.FromUserId == currentUserId && x.ToUserId == userId);
        }

        public async Task<bool> GetBlockStatus(Guid currentUserId, Guid toUserId)
        {
            return await _context.BlockUsers.AnyAsync(x => x.FromUserId == currentUserId && x.ToUserId == toUserId);
        }

        public int CalculateAge(DateTime birthDay)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age))
                age--;

            return age;
        }

        public void FilterUers(ref List<AppUser> users, FilterUserViewModel filter)
        {
            if (filter.Location != null && filter.Location != "")
            {
                if (Enum.TryParse(filter.Location.Trim(), out ELocation locate))
                {
                    users = users.Where(x => x.Location == locate).ToList();
                }
            }

            if (filter.FullName != null && filter.FullName.Trim() != "")
            {
                users = users.Where(x => x.FullName.Contains(filter.FullName.Trim())).ToList();
            }

            if (filter.Gender != null && filter.Gender.Trim() != "")
            {
                if (Enum.TryParse(filter.Gender.Trim(), out EGender gender))
                    users = users.Where(x => x.Gender == gender).ToList();
            }

            if (filter.FromAge != 0)
            {
                users = users.Where(x => CalculateAge(x.Dob) >= filter.FromAge).ToList();
            }

            if (filter.ToAge != 0)
            {
                users = users.Where(x => CalculateAge(x.Dob) <= filter.ToAge).ToList();
            }
        }

        public async Task<List<UserDisplay>> GetUserDisplay(List<AppUser> users, bool nonImage = false)
        {
            var userDisplays = new List<UserDisplay>();
            //Get Session user - Check login
            var isLogin = true;
            var sessionUser = _sessionService.GetDataFromToken();
            if (sessionUser == null)
            {
                isLogin = false;
                sessionUser = new LoginInfo()
                {
                    UserId = Guid.NewGuid()
                };
            }

            foreach (var user in users)
            {
                var userDisplay = new UserDisplay(user, _storageService, nonImage);

                if (!nonImage)
                {
                    userDisplay.Followed = await IsFollowed(user.Id, sessionUser.UserId);
                    userDisplay.Favorited = await IsLiked(user.Id, sessionUser.UserId);
                }

                userDisplays.Add(userDisplay);
            }
            return userDisplays;
        }

        public async Task<AppUser> BidingUserRequest(AppUser user, UserRequest request)
        {
            if (Enum.TryParse(request.Gender, out EGender gender))
            {
                user.Gender = gender;
            }

            if (Enum.TryParse(request.Cook, out ECook cook))
            {
                user.Cook = cook;
            }

            if (Enum.TryParse(request.Game, out EGame game))
            {
                user.Game = game;
            }

            if (Enum.TryParse(request.Travel, out ETravel travel))
            {
                user.Travel = travel;
            }

            if (Enum.TryParse(request.Shopping, out EShopping shopping))
            {
                user.Shopping = shopping;
            }

            if (Enum.TryParse(request.Location, out ELocation location))
            {
                user.Location = location;
            }

            if (Enum.TryParse(request.Body, out EBody body))
            {
                user.Body = body;
            }

            if (Enum.TryParse(request.Target, out ETarget target))
            {
                user.Target = target;
            }

            if (Enum.TryParse(request.Education, out EEducation education))
            {
                user.Education = education;
            }


            if (Enum.TryParse(request.LikePet, out ELikePet pet))
            {
                user.LikePet = pet;
            }

            if (Enum.TryParse(request.LikeTechnology, out ELikeTechnology technology))
            {
                user.LikeTechnology = technology;
            }

            if (Enum.TryParse(request.PlaySport, out EPlaySport playSport))
            {
                user.PlaySport = playSport;
            }

            if (Enum.TryParse(request.Character, out ECharacter character))
            {
                user.Character = character;
            }

            if (Enum.TryParse(request.LifeStyle, out ELifeStyle lifeStyle))
            {
                user.LifeStyle = lifeStyle;
            }

            if (Enum.TryParse(request.MostValuable, out EMostValuable mostValuable))
            {
                user.MostValuable = mostValuable;
            }

            if (Enum.TryParse(request.Marriage, out EMarriage marriage))
            {
                user.Marriage = marriage;
            }

            if (Enum.TryParse(request.Job, out EJob job))
            {
                user.Job = job;
            }

            if (Enum.TryParse(request.Religion, out EReligion religion))
            {
                user.Religion = religion;
            }

            if (Enum.TryParse(request.FavoriteMovie, out EFavoriteMovie favoriteMovie))
            {
                user.FavoriteMovie = favoriteMovie;
            }

            if (Enum.TryParse(request.AtmosphereLike, out EAtmosphereLike atmosphereLike))
            {
                user.AtmosphereLike = atmosphereLike;
            }

            if (Enum.TryParse(request.Smoking, out ESmoking smoking))
            {
                user.Smoking = smoking;
            }

            if (Enum.TryParse(request.DrinkBeer, out EDrinkBeer drinkBeer))
            {
                user.DrinkBeer = drinkBeer;
            }

            //

            if (request.PhoneNumber != "" && request.PhoneNumber != null)
            {
                user.PhoneNumber = request.PhoneNumber;
            }

            if (request.FullName != "" && request.FullName != null)
            {
                user.FullName = request.FullName;
            }

            if (request.Title != "" && request.Title != null)
            {
                user.Title = request.Title;
            }

            if (request.Summary != "" && request.Summary != null)
            {
                user.Summary = request.Summary;
            }

            if (request.Weight >= 20 && request.Weight <= 200)
            {
                user.Weight = request.Weight;
            }

            if (request.Height >= 120 && request.Height <= 200)
            {
                user.Height = request.Height;
            }

            if (CalculateAge(request.Dob) >= 10 && CalculateAge(request.Dob) <= 100)
            {
                user.Dob = request.Dob;
            }

            if (request.AvatarFile != null)
            {
                user.AvatarPath = await this.SaveFile(request.AvatarFile);
            }
            return user;
        }

        public EAgeGroup GetAgeGroup(DateTime birthDay)
        {
            int age = CalculateAge(birthDay);
            if (age < 18)
                return EAgeGroup.Dưới_18_Tuổi;
            else if (age < 26)
                return EAgeGroup.Từ_18_Đến_25;
            else if (age < 31)
                return EAgeGroup.Từ_25_Đến_30;
            else if (age < 41)
                return EAgeGroup.Từ_31_Đến_40;
            else if (age < 51)
                return EAgeGroup.Từ_41_Đến_50;
            else return EAgeGroup.Trên_50;
        }

        public async Task<AppUser> GetById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> UpdateUser(AppUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
