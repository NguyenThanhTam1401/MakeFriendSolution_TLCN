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

        public UserApplication()
        {
        }

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

            if (filter.ToAge > filter.FromAge)
            {
                users = users.Where(x => CalculateAge(x.Dob) <= filter.ToAge).ToList();
            }
        }

        public async Task<List<UserDisplay>> GetUserDisplay(List<AppUser> users, bool nonImage = false)
        {
            var userDisplays = new List<UserDisplay>();
            //Get Session user - Check login
            var sessionUser = _sessionService.GetDataFromToken();
            if (sessionUser == null)
            {
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

        public async Task<AppUser> UpdateUser(AppUser user, bool isUpdateScore)
        {
            var updateScore = await _context.SimilariryFeatures.FirstOrDefaultAsync();
            if (isUpdateScore)
            {
                updateScore.UpdatedAt = DateTime.Now;
                _context.SimilariryFeatures.Update(updateScore);
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> IsExist(Guid userId)
        {
            return await _context.Users.AnyAsync(x => x.Id == userId);
        }

        public async Task<List<AppUser>> GetActiveUsers(Guid userId, bool getLock)
        {
            if (!getLock)
            {
                return await _context.Users
                .Where(x => x.Status == Models.Enum.EUserStatus.Active && x.IsInfoUpdated)
                .ToListAsync();
            }

            var blocksFrom = await _context.BlockUsers.Where(x => x.FromUserId == userId).ToListAsync();
            var blocksTo = await _context.BlockUsers.Where(x => x.ToUserId == userId).ToListAsync();
            List<Guid> blocksId = new List<Guid>();
            foreach (var item in blocksFrom)
            {
                blocksId.Add(item.ToUserId);
            }

            foreach (var item in blocksTo)
            {
                blocksId.Add(item.FromUserId);
            }

            var users = await _context.Users
                .Where(x => x.Status == Models.Enum.EUserStatus.Active && x.IsInfoUpdated)
                .ToListAsync();

            foreach (var item in blocksId)
            {
                users.Remove(users.Where(x => x.Id == item).FirstOrDefault());
            }

            return users;
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Favorite> GetFavoriteById(Guid fromId, Guid toId)
        {
            return await _context.Favorites
                .Where(x => x.FromUserId == fromId && x.ToUserId == toId)
                .FirstOrDefaultAsync();
        }

        public async Task<string> DisableUser(Guid userId)
        {
            string message = "Do nothing";
            var user = await _context.Users.FindAsync(userId);

            if (user.Status == EUserStatus.Active)
            {
                user.Status = EUserStatus.Inactive;
                message = "Blocked";
            }
            else if (user.Status == EUserStatus.Inactive)
            {
                user.Status = EUserStatus.Active;
                message = "Unclocked";
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> UpdateSimilarityScores(Guid userId)
        {
            var user = await GetById(userId);
            var oldScores = await _context.SimilarityScores.Where(x => x.FromUserId == userId).ToListAsync();
            var tempUsers = new List<AppUser>();
            var users = await GetActiveUsers(userId, true);
            users.Remove(users.Where(x => x.Id == userId).FirstOrDefault());
            //var users = await _context.Users.Where(x => x.Status == EUserStatus.Active && x.IsInfoUpdated && x.Id != userId).ToListAsync();

            //
            tempUsers = users
                .Where(x => x.Id != userId && x.Gender == user.FindPeople)
                .ToList();
            users = new List<AppUser>();
            var userAgeGroup = GetAgeGroup(user.Dob);
            int userAgeValue = Convert.ToInt32(userAgeGroup);

            foreach (var item in tempUsers)
            {
                var ageGroup = GetAgeGroup(item.Dob);
                int ageValue = Convert.ToInt32(ageGroup);

                if (Math.Abs(userAgeValue - ageValue) <= 1)
                {
                    users.Add(item);
                }
            }

            //
            users.Insert(0, user);
            int sl = users.Count;

            double[,] usersMatrix = new double[sl, 19];
            for (int i = 0; i < sl; i++)
            {
                usersMatrix[i, 0] = (double)users[i].Marriage;
                usersMatrix[i, 1] = (double)users[i].Target;
                usersMatrix[i, 2] = (double)users[i].Education;
                usersMatrix[i, 3] = (double)users[i].Body;
                usersMatrix[i, 4] = (double)users[i].Religion;
                usersMatrix[i, 5] = (double)users[i].Smoking;
                usersMatrix[i, 6] = (double)users[i].DrinkBeer;
                usersMatrix[i, 7] = (double)users[i].Cook;
                usersMatrix[i, 8] = (double)users[i].Game;
                usersMatrix[i, 9] = (double)users[i].Travel;
                usersMatrix[i, 10] = (double)users[i].LikePet;
                usersMatrix[i, 11] = (double)users[i].LikeTechnology;
                usersMatrix[i, 12] = (double)users[i].Shopping;
                usersMatrix[i, 13] = (double)users[i].PlaySport;
                usersMatrix[i, 14] = (double)users[i].FavoriteMovie;
                usersMatrix[i, 15] = (double)users[i].AtmosphereLike;
                usersMatrix[i, 16] = (double)users[i].Character;
                usersMatrix[i, 17] = (double)users[i].LifeStyle;
                usersMatrix[i, 18] = (double)users[i].MostValuable;
            }

            SimilarityMatrix m = new SimilarityMatrix();
            m.Row = sl;
            m.Column = 19;
            m.Matrix = usersMatrix;

            List<double> kq = new List<double>();
            kq = m.SimilarityCalculate();

            users.RemoveAt(0);

            var similarityScores = new List<SimilarityScore>();

            for (int i = 1; i < kq.Count; i++)
            {
                users[i - 1].Point = kq[i];
                var score = new SimilarityScore()
                {
                    FromUserId = userId,
                    ToUserId = users[i - 1].Id,
                    Score = kq[i]
                };
                similarityScores.Add(score);
            }
            try
            {
                _context.SimilarityScores.AddRange(similarityScores);
                _context.SimilarityScores.RemoveRange(oldScores);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Tuple<List<UserDisplay>, int>> GetSimilarityScores(Guid userId, FilterUserViewModel request)
        {
            List<UserDisplay> userResponses = new List<UserDisplay>();

            if (!request.IsFilter)
            {
                var tempScore = await _context.SimilarityScores.Where(x => x.FromUserId == userId)
                    .OrderByDescending(x => x.Score)
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize).ToListAsync();

                List<AppUser> users = new List<AppUser>();
                foreach (var item in tempScore)
                {
                    var user = await GetById(item.ToUserId);
                    user.Point = item.Score;
                    users.Add(user);
                }
                userResponses = await GetUserDisplay(users);
                var total = tempScore.Count / request.PageSize;
                return Tuple.Create(userResponses, total);
            }
            else
            {
                var userInfo = await GetById(userId);
                var users = await GetActiveUsers(userId, true);
                FilterUers(ref users, request);

                var total = users.Count / request.PageSize;
                users = users.OrderByDescending(x => x.Point)
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize).ToList();

                userResponses = await GetUserDisplay(users);

                return Tuple.Create(userResponses, total);
            }
        }

        public async Task<Tuple<List<UserDisplay>, int>> GetSimilarityUsers(Guid userId, FilterUserViewModel request)
        {
            var updateScore = await _context.SimilariryFeatures.FirstOrDefaultAsync();
            var user = await GetById(userId);
            if (updateScore.UpdatedAt > user.UpdatedAt)
            {
                var update = await UpdateSimilarityScores(userId);
                user.UpdatedAt = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            var result = await GetSimilarityScores(userId, request);

            return result;
        }
    }
}