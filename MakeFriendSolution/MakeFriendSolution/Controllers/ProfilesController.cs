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
using MakeFriendSolution.Application;

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private ISessionService _sessionService;
        private readonly IUserApplication _userApplication;

        public ProfilesController(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService, IUserApplication userApplication)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
            _userApplication = userApplication;
        }
        [AllowAnonymous]
        [HttpGet("data")]
        public IActionResult GenerateData()
        {
            var tenNam = new List<string>();
            var tenNu = new List<string>();
            var ho = Constain.ho;

            foreach (var item in Constain.tenNam)
            {
                var trim = item.Trim();
                if (trim != "")
                {
                    tenNam.Add(trim);
                }
            }

            foreach (var item in Constain.tenNu)
            {
                var trim = item.Trim();
                if (trim != "")
                {
                    tenNu.Add(trim);
                }
            }

            List<AppUser> users = new List<AppUser>();
            int hoSize = ho.Count;
            int tenNamSize = tenNam.Count;
            int tenNuSize = tenNu.Count;
            Random random = new Random();
            int gmailCount = 1;
            //Random Nu
            for (int i = 0; i < 1000; i++)
            {
                var user = new AppUser();
                user.FullName = ho[random.Next(0, hoSize)] + " " + tenNu[random.Next(0, tenNuSize)];
                user.Gender = EGender.Nữ;
                user.AtmosphereLike = RandomEnumValue<EAtmosphereLike>();
                user.Body = RandomEnumValue<EBody>();
                user.Character = RandomEnumValue<ECharacter>();
                user.Cook = RandomEnumValue<ECook>();
                user.DrinkBeer = RandomEnumValue<EDrinkBeer>();
                user.Education = RandomEnumValue<EEducation>();
                user.Email = (gmailCount++).ToString() + "@gmail.com";
                user.FavoriteMovie = RandomEnumValue<EFavoriteMovie>();
                user.FindPeople = RandomEnumValue<EGender>();
                user.Game = RandomEnumValue<EGame>();
                user.Height = random.Next(145, 180);
                user.Weight = random.Next(30, 70);
                user.IsInfoUpdated = true;
                user.Job = RandomEnumValue<EJob>();
                user.LifeStyle = RandomEnumValue<ELifeStyle>();
                user.LikePet = RandomEnumValue<ELikePet>();
                user.LikeTechnology = RandomEnumValue<ELikeTechnology>();
                user.Location = RandomEnumValue<ELocation>();
                user.Marriage = RandomEnumValue<EMarriage>();
                user.MostValuable = RandomEnumValue<EMostValuable>();
                user.PassWord = "1111";
                user.PhoneNumber = "+84" + (random.Next(100000000, 999999999));
                user.PlaySport = RandomEnumValue<EPlaySport>();
                user.Religion = RandomEnumValue<EReligion>();
                user.Role = ERole.User;
                user.Shopping = RandomEnumValue<EShopping>();
                user.Smoking = RandomEnumValue<ESmoking>();
                user.Status = EUserStatus.Active;
                user.Summary = "Mình là " + user.FullName + ", kết bạn với mình nhé!";
                user.Target = RandomEnumValue<ETarget>();
                user.Title = "Kết bạn với " + user.FullName + " nhé!";
                user.Travel = RandomEnumValue<ETravel>();
                user.TypeAccount = ETypeAccount.System;
                user.UserName = user.Email;
                user.FavoriteMovie = RandomEnumValue<EFavoriteMovie>();
                user.NumberOfLikes = random.Next(0, 300);

                user.AvatarPath = "women/" + random.Next(101, 300) + ".jpg";
               
                int day = random.Next(1, 28);
                int month = random.Next(1, 12);
                int year = random.Next(1970, 2006);

                int createdDay = random.Next(1, 28);
                int createdMonth = random.Next(1, 12);
                int createdYear = random.Next(2018, 2020);

                var dob = new DateTime(year, month, day);
                var createdDate = new DateTime(createdYear, createdMonth, createdDay);

                user.CreatedAt = createdDate;
                user.Dob = dob.Date;

                users.Add(user);
            }
            //Random Nam
            for (int i = 0; i < 1000; i++)
            {
                var user = new AppUser();
                user.FullName = ho[random.Next(0, hoSize)] + " " + tenNam[random.Next(0, tenNamSize)];
                user.Gender = EGender.Nam;
                user.AtmosphereLike = RandomEnumValue<EAtmosphereLike>();
                user.Body = RandomEnumValue<EBody>();
                user.Character = RandomEnumValue<ECharacter>();
                user.Cook = RandomEnumValue<ECook>();
                user.DrinkBeer = RandomEnumValue<EDrinkBeer>();
                user.Education = RandomEnumValue<EEducation>();
                user.Email = (gmailCount++).ToString() + "@gmail.com";
                user.FavoriteMovie = RandomEnumValue<EFavoriteMovie>();
                user.FindPeople = RandomEnumValue<EGender>();
                user.Game = RandomEnumValue<EGame>();
                user.Height = random.Next(150, 200);
                user.Weight = random.Next(40, 80);
                user.IsInfoUpdated = true;
                user.Job = RandomEnumValue<EJob>();
                user.LifeStyle = RandomEnumValue<ELifeStyle>();
                user.LikePet = RandomEnumValue<ELikePet>();
                user.LikeTechnology = RandomEnumValue<ELikeTechnology>();
                user.Location = RandomEnumValue<ELocation>();
                user.Marriage = RandomEnumValue<EMarriage>();
                user.MostValuable = RandomEnumValue<EMostValuable>();
                user.PassWord = "1111";
                user.PhoneNumber = "+84" + (random.Next(100000000, 999999999));
                user.PlaySport = RandomEnumValue<EPlaySport>();
                user.Religion = RandomEnumValue<EReligion>();
                user.Role = ERole.User;
                user.Shopping = RandomEnumValue<EShopping>();
                user.Smoking = RandomEnumValue<ESmoking>();
                user.Status = EUserStatus.Active;
                user.Summary = "Mình là " + user.FullName + ", kết bạn với mình nhé!";
                user.Target = RandomEnumValue<ETarget>();
                user.Title = "Kết bạn với " + user.FullName + " nhé!";
                user.Travel = RandomEnumValue<ETravel>();
                user.TypeAccount = ETypeAccount.System;
                user.UserName = user.Email;
                user.FavoriteMovie = RandomEnumValue<EFavoriteMovie>();
                user.NumberOfLikes = random.Next(0, 300);

                user.AvatarPath = "men/" + random.Next(1, 100) + ".jpg";

                int day = random.Next(1, 28);
                int month = random.Next(1, 12);
                int year = random.Next(1970, 2006);

                var dob = new DateTime(year, month, day);

                user.Dob = dob.Date;

                users.Add(user);
            }

            //var response = new List<UserResponse>();
            //foreach (var item in users)
            //{
            //    var u = new UserResponse(item, _storageService);
            //    response.Add(u);
            //}

            _context.Users.AddRange(users);
            _context.SaveChanges();
            return Ok();
        }

        public static T RandomEnumValue<T>()
        {
            Random random = new Random();
            var values = Enum.GetValues(typeof(T));
            int num =random.Next(0, values.Length);
            return (T)values.GetValue(num);
        }

        [HttpGet("similar/{userId}")]
        public async Task<IActionResult> GetMatrix(Guid userId, [FromQuery] FilterUserViewModel filter)
        {
            var user = await _context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            var tempUsers = await _context.Users
                .Where(x => x.Id != userId && x.FindPeople == user.Gender && x.Status == EUserStatus.Active && x.IsInfoUpdated)
                .ToListAsync();

            var users = new List<AppUser>();

            var userAgeGroup = _userApplication.GetAgeGroup(user.Dob);
            int userAgeValue = Convert.ToInt32(userAgeGroup);

            foreach (var item in tempUsers)
            {
                var ageGroup = _userApplication.GetAgeGroup(item.Dob);
                int ageValue = Convert.ToInt32(ageGroup);

                if (Math.Abs(userAgeValue - ageValue) <= 1)
                {
                    users.Add(item);
                }
            }

            //FilterUsers
            _userApplication.FilterUers(ref users, filter);

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

            cMatrix m = new cMatrix();
            m.Row = sl;
            m.Column = 19;
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

            var usersDisplay = await _userApplication.GetUserDisplay(users);

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

            user = await _userApplication.BidingUserRequest(user, request);
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

            var Cook = new List<string>();
            List<ECook> Cooks = Enum.GetValues(typeof(ECook))
                    .Cast<ECook>()
                    .ToList();
            foreach (var item in Cooks)
            {
                Cook.Add(item.ToString());
            }

            var Game = new List<string>();
            List<EGame> Games = Enum.GetValues(typeof(EGame))
                    .Cast<EGame>()
                    .ToList();
            foreach (var item in Games)
            {
                Game.Add(item.ToString());
            }

            var Travel = new List<string>();
            List<ETravel> Travels = Enum.GetValues(typeof(ETravel))
                    .Cast<ETravel>()
                    .ToList();
            foreach (var item in Travels)
            {
                Travel.Add(item.ToString());
            }

            var PlaySport = new List<string>();
            List<EPlaySport> LikeSports = Enum.GetValues(typeof(EPlaySport))
                    .Cast<EPlaySport>()
                    .ToList();
            foreach (var item in LikeSports)
            {
                PlaySport.Add(item.ToString());
            }

            var Shopping = new List<string>();
            List<EShopping> Shoppings = Enum.GetValues(typeof(EShopping))
                    .Cast<EShopping>()
                    .ToList();
            foreach (var item in Shoppings)
            {
                Shopping.Add(item.ToString());
            }

            var LikePet = new List<string>();
            List<ELikePet> LikePets = Enum.GetValues(typeof(ELikePet))
                    .Cast<ELikePet>()
                    .ToList();
            foreach (var item in LikePets)
            {
                LikePet.Add(item.ToString());
            }

            var LikeTechnology = new List<string>();
            List<ELikeTechnology> LikeTechnologies = Enum.GetValues(typeof(ELikeTechnology))
                    .Cast<ELikeTechnology>()
                    .ToList();
            foreach (var item in LikeTechnologies)
            {
                LikeTechnology.Add(item.ToString());
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
                Cook,
                LikeTechnology,
                LikePet,
                PlaySport,
                Travel,
                Game,
                Shopping,
                TypeAccount,
                UserStatus
            };

            return Ok(response);
        }

    }
}