using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Common;
using MakeFriendSolution.Models.Enum;
using MakeFriendSolution.Models.ViewModels;
using System.Net.Http.Headers;
using System.IO;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Authorization;

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private ISessionService _sessionService;

        public ProfilesController(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
        }

        [HttpGet("similar/{userId}")]
        public async Task<IActionResult> GetMatrix(Guid userId, [FromQuery] FilterUserViewModel filter)
        {
            var user = await _context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            var users = await _context.Users
                .Where(x => x.Id != userId && x.FindPeople == user.Gender && x.Status == EUserStatus.Active && x.IsInfoUpdated)
                .ToListAsync();

            //FilterUsers
            FilterUers(ref users, filter);

            users.Insert(0, user);

            int sl = users.Count;

            double[,] usersMatrix = new double[sl, 12];
            for (int i = 0; i < sl; i++)
            {
                usersMatrix[i, 0] = (double)users[i].Marriage;
                usersMatrix[i, 1] = (double)users[i].Target;
                usersMatrix[i, 2] = (double)users[i].Education;
                usersMatrix[i, 3] = (double)users[i].Body;
                usersMatrix[i, 4] = (double)users[i].Religion;
                usersMatrix[i, 5] = (double)users[i].Smoking;
                usersMatrix[i, 6] = (double)users[i].DrinkBeer;
                usersMatrix[i, 7] = (double)users[i].FavoriteMovie;
                usersMatrix[i, 8] = (double)users[i].AtmosphereLike;
                usersMatrix[i, 9] = (double)users[i].Character;
                usersMatrix[i, 10] = (double)users[i].LifeStyle;
                usersMatrix[i, 11] = (double)users[i].MostValuable;
            }

            cMatrix m = new cMatrix();
            m.Row = sl;
            m.Column = 12;
            m.Matrix = usersMatrix;

            List<double> kq = new List<double>();
            kq = m.SimilarityCalculate();

            //users.RemoveAt(0);

            for (int i = 0; i < kq.Count; i++)
            {
                users[i].Point = kq[i];
            }

            users = users.OrderByDescending(o => o.Point).ToList();

            users = users
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize).ToList();

            var usersDisplay = await this.GetUserDisplay(users);

            return Ok(usersDisplay);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UserRequest request)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "Can not find User with Id = " + request.Id
                });
            }

            user = await this.BidingUserRequest(user, request);
            try
            {
                user.IsInfoUpdated = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(new
                {
                    Message = e.InnerException
                });
            }

            var response = new UserResponse(user, _storageService);

            return Ok(response);
        }

        private async Task<AppUser> BidingUserRequest(AppUser user, UserRequest request)
        {
            if (Enum.TryParse(request.Gender, out EGender gender))
            {
                user.Gender = gender;
            }

            if (Enum.TryParse(request.Location, out ELocation location))
            {
                user.Location = location;
            }

            if (Enum.TryParse(request.FindPeople, out EGender findPeople))
            {
                user.FindPeople = findPeople;
            }

            if (Enum.TryParse(request.Marriage, out EMarriage marriage))
            {
                user.Marriage = marriage;
            }

            if (Enum.TryParse(request.Target, out ETarget target))
            {
                user.Target = target;
            }

            if (Enum.TryParse(request.Education, out EEducation education))
            {
                user.Education = education;
            }

            if (Enum.TryParse(request.Body, out EBody body))
            {
                user.Body = body;
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

        private int CalculateAge(DateTime birthDay)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age))
                age--;

            return age;
        }

        private void FilterUers(ref List<AppUser> users, FilterUserViewModel filter)
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

        //Save File
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        [HttpGet("updateProfile")]
        public async Task<IActionResult> ChuanHoaDob()
        {
            Random random = new Random();
            var users = await _context.Users
                .Where(x => x.Dob.Year > 2010).ToListAsync();
            foreach (var user in users)
            {
                user.Dob = user.Dob.AddYears(-random.Next(15, 60));
            }

            try
            {
                _context.UpdateRange(users);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                var message = e.InnerException;
                return BadRequest(message);
            }
            return Ok("Profiles had been updated!");
        }

        [AllowAnonymous]
        [HttpGet("features")]
        public IActionResult GetFeatures()
        {
            var AtmosphereLike = new List<string>();
            List<EAtmosphereLike> AtmosphereLikes = Enum.GetValues(typeof(EAtmosphereLike))
                    .Cast<EAtmosphereLike>()
                    .ToList();
            foreach (var item in AtmosphereLikes)
            {
                AtmosphereLike.Add(item.ToString());
            }

            var Body = new List<string>();
            List<EBody> Bodys = Enum.GetValues(typeof(EBody))
                    .Cast<EBody>()
                    .ToList();
            foreach (var item in Bodys)
            {
                Body.Add(item.ToString());
            }

            var Character = new List<string>();
            List<ECharacter> Characters = Enum.GetValues(typeof(ECharacter))
                    .Cast<ECharacter>()
                    .ToList();
            foreach (var item in Characters)
            {
                Character.Add(item.ToString());
            }

            var DrinkBeer = new List<string>();
            List<EDrinkBeer> DrinkBeers = Enum.GetValues(typeof(EDrinkBeer))
                    .Cast<EDrinkBeer>()
                    .ToList();
            foreach (var item in DrinkBeers)
            {
                DrinkBeer.Add(item.ToString());
            }

            var Education = new List<string>();
            List<EEducation> Educations = Enum.GetValues(typeof(EEducation))
                    .Cast<EEducation>()
                    .ToList();
            foreach (var item in Educations)
            {
                Education.Add(item.ToString());
            }

            var FavoriteMovie = new List<string>();
            List<EFavoriteMovie> FavoriteMovies = Enum.GetValues(typeof(EFavoriteMovie))
                    .Cast<EFavoriteMovie>()
                    .ToList();
            foreach (var item in FavoriteMovies)
            {
                FavoriteMovie.Add(item.ToString());
            }

            var Gender = new List<string>();
            List<EGender> Genders = Enum.GetValues(typeof(EGender))
                    .Cast<EGender>()
                    .ToList();
            foreach (var item in Genders)
            {
                Gender.Add(item.ToString());
            }

            var FindPeople = new List<string>();
            List<EGender> findPeople = Enum.GetValues(typeof(EGender))
                    .Cast<EGender>()
                    .ToList();
            foreach (var item in findPeople)
            {
                Gender.Add(item.ToString());
            }

            var Job = new List<string>();
            List<EJob> Jobs = Enum.GetValues(typeof(EJob))
                    .Cast<EJob>()
                    .ToList();
            foreach (var item in Jobs)
            {
                Job.Add(item.ToString());
            }

            var LifeStyle = new List<string>();
            List<ELifeStyle> LifeStyles = Enum.GetValues(typeof(ELifeStyle))
                    .Cast<ELifeStyle>()
                    .ToList();
            foreach (var item in LifeStyles)
            {
                LifeStyle.Add(item.ToString());
            }

            var Location = new List<string>();
            List<ELocation> Locations = Enum.GetValues(typeof(ELocation))
                    .Cast<ELocation>()
                    .ToList();
            foreach (var item in Locations)
            {
                Location.Add(item.ToString());
            }

            var Marriage = new List<string>();
            List<EMarriage> Marriages = Enum.GetValues(typeof(EMarriage))
                    .Cast<EMarriage>()
                    .ToList();
            foreach (var item in Marriages)
            {
                Marriage.Add(item.ToString());
            }

            var MostValuable = new List<string>();
            List<EMostValuable> MostValuables = Enum.GetValues(typeof(EMostValuable))
                    .Cast<EMostValuable>()
                    .ToList();
            foreach (var item in MostValuables)
            {
                MostValuable.Add(item.ToString());
            }

            var OperationType = new List<string>();
            List<EOperationType> OperationTypes = Enum.GetValues(typeof(EOperationType))
                    .Cast<EOperationType>()
                    .ToList();
            foreach (var item in OperationTypes)
            {
                OperationType.Add(item.ToString());
            }

            var Religion = new List<string>();
            List<EReligion> Religions = Enum.GetValues(typeof(EReligion))
                    .Cast<EReligion>()
                    .ToList();
            foreach (var item in Religions)
            {
                Religion.Add(item.ToString());
            }

            var Smoking = new List<string>();
            List<ESmoking> Smokings = Enum.GetValues(typeof(ESmoking))
                    .Cast<ESmoking>()
                    .ToList();
            foreach (var item in Smokings)
            {
                Smoking.Add(item.ToString());
            }

            var Target = new List<string>();
            List<ETarget> Targets = Enum.GetValues(typeof(ETarget))
                    .Cast<ETarget>()
                    .ToList();
            foreach (var item in Targets)
            {
                Target.Add(item.ToString());
            }

            var TypeAccount = new List<string>();
            List<ETypeAccount> TypeAccounts = Enum.GetValues(typeof(ETypeAccount))
                    .Cast<ETypeAccount>()
                    .ToList();
            foreach (var item in TypeAccounts)
            {
                TypeAccount.Add(item.ToString());
            }

            var UserStatus = new List<string>();
            List<EUserStatus> UserStatuses = Enum.GetValues(typeof(EUserStatus))
                    .Cast<EUserStatus>()
                    .ToList();
            foreach (var item in UserStatuses)
            {
                UserStatus.Add(item.ToString());
            }

            var response = new
            {
                AtmosphereLike,
                Body,
                Character,
                DrinkBeer,
                Education,
                FavoriteMovie,
                Gender,
                FindPeople,
                Job,
                LifeStyle,
                Location,
                Marriage,
                MostValuable,
                OperationType,
                Religion,
                Smoking,
                Target,
                TypeAccount,
                UserStatus
            };

            return Ok(response);
        }

        private async Task<List<UserDisplay>> GetUserDisplay(List<AppUser> users)
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
                var userDisplay = new UserDisplay(user, this._storageService);

                var followResult = await this.GetNumberOfFollowers(userDisplay.Id, isLogin, sessionUser.UserId);
                userDisplay.NumberOfFollowers = followResult.Item1;
                userDisplay.Followed = followResult.Item2;

                var favoriteResult = await this.GetNumberOfFavoritors(userDisplay.Id, isLogin, sessionUser.UserId);
                userDisplay.NumberOfFavoritors = favoriteResult.Item1;
                userDisplay.Favorited = favoriteResult.Item2;

                userDisplay.NumberOfImages = await this.GetNumberOfImages(user.Id);

                userDisplays.Add(userDisplay);
            }
            return userDisplays;
        }

        private async Task<(int, bool)> GetNumberOfFollowers(Guid userId, bool isLogin, Guid currentUserId)
        {
            var numberOfFollowers = await _context.Follows.Where(x => x.ToUserId == userId).CountAsync();
            bool followed = false;
            if (isLogin)
            {
                followed = await _context.Follows.AnyAsync(x => x.FromUserId == currentUserId && x.ToUserId == userId);
            }

            return (numberOfFollowers, followed);
        }

        private async Task<(int, bool)> GetNumberOfFavoritors(Guid userId, bool isLogin, Guid currentUserId)
        {
            var numberOfFavoritors = await _context.Favorites.Where(x => x.ToUserId == userId).CountAsync();
            bool favorited = false;
            if (isLogin)
            {
                favorited = await _context.Favorites.AnyAsync(x => x.FromUserId == currentUserId && x.ToUserId == userId);
            }

            return (numberOfFavoritors, favorited);
        }

        public async Task<int> GetNumberOfImages(Guid userId)
        {
            return await _context.ThumbnailImages.Where(x => x.UserId == userId).CountAsync();
        }
    }
}