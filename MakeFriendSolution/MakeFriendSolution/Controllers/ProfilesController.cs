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
            for (int i = 0; i < 468; i++)
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
                user.TypeAccount = ETypeAccount.Facebook;
                user.UserName = user.Email;
                user.FavoriteMovie = RandomEnumValue<EFavoriteMovie>();
                user.NumberOfLikes = random.Next(10, 10000);

                user.AvatarPath = "women/" + random.Next(101, 300) + ".jpg";

                int day = random.Next(1, 29);
                int month = random.Next(1, 13);
                int year = random.Next(1970, 2007);

                int createdDay = random.Next(1, 29);
                int createdMonth = random.Next(1, 13);
                int createdYear = random.Next(2018, 2021);

                var dob = new DateTime(year, month, day);
                var createdDate = new DateTime(createdYear, createdMonth, createdDay);

                user.CreatedAt = createdDate;
                user.Dob = dob.Date;

                users.Add(user);
            }
            //Random Nam
            for (int i = 0; i < 417; i++)
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
                user.TypeAccount = ETypeAccount.Facebook;
                user.UserName = user.Email;
                user.FavoriteMovie = RandomEnumValue<EFavoriteMovie>();
                user.NumberOfLikes = random.Next(10, 10000);

                user.AvatarPath = "men/" + random.Next(1, 100) + ".jpg";

                int day = random.Next(1, 29);
                int month = random.Next(1, 13);
                int year = random.Next(1970, 2007);

                int createdDay = random.Next(1, 29);
                int createdMonth = random.Next(1, 13);
                int createdYear = random.Next(2018, 2021);

                var dob = new DateTime(year, month, day);
                var createdDate = new DateTime(createdYear, createdMonth, createdDay);
                user.Dob = dob.Date;
                user.CreatedAt = createdDate.Date;

                users.Add(user);
            }

            //Random Nu
            for (int i = 0; i < 641; i++)
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
                user.NumberOfLikes = random.Next(10, 10000);

                user.AvatarPath = "women/" + random.Next(101, 300) + ".jpg";

                int day = random.Next(1, 29);
                int month = random.Next(1, 13);
                int year = random.Next(1970, 2007);

                int createdDay = random.Next(1, 29);
                int createdMonth = random.Next(1, 13);
                int createdYear = random.Next(2018, 2021);

                var dob = new DateTime(year, month, day);
                var createdDate = new DateTime(createdYear, createdMonth, createdDay);

                user.CreatedAt = createdDate;
                user.Dob = dob.Date;

                users.Add(user);
            }
            //Random Nam
            for (int i = 0; i < 623; i++)
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
                user.NumberOfLikes = random.Next(10, 10000);

                user.AvatarPath = "men/" + random.Next(1, 100) + ".jpg";

                int day = random.Next(1, 29);
                int month = random.Next(1, 13);
                int year = random.Next(1970, 2007);

                int createdDay = random.Next(1, 29);
                int createdMonth = random.Next(1, 13);
                int createdYear = random.Next(2018, 2021);

                var dob = new DateTime(year, month, day);
                var createdDate = new DateTime(createdYear, createdMonth, createdDay);
                user.Dob = dob.Date;
                user.CreatedAt = createdDate.Date;

                users.Add(user);
            }

            //Random InActive Account
            for (int i = 0; i < 360; i++)
            {
                var user = new AppUser();
                user.FullName = ho[random.Next(0, hoSize)] + " " + tenNu[random.Next(0, tenNuSize)];
                user.Gender = RandomEnumValue<EGender>();
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
                user.Status = RandomEnumValue<EUserStatus>();
                user.Summary = "Mình là " + user.FullName + ", kết bạn với mình nhé!";
                user.Target = RandomEnumValue<ETarget>();
                user.Title = "Kết bạn với " + user.FullName + " nhé!";
                user.Travel = RandomEnumValue<ETravel>();
                user.TypeAccount = ETypeAccount.Google;
                user.UserName = user.Email;
                user.FavoriteMovie = RandomEnumValue<EFavoriteMovie>();
                user.NumberOfLikes = random.Next(10, 10000);

                if (user.Gender == EGender.Nữ)
                {
                    user.AvatarPath = "women/" + random.Next(101, 300) + ".jpg";
                }
                else
                {
                    user.AvatarPath = "men/" + random.Next(1, 100) + ".jpg";
                }

                int day = random.Next(1, 29);
                int month = random.Next(1, 13);
                int year = random.Next(1970, 2007);

                int createdDay = random.Next(1, 29);
                int createdMonth = random.Next(1, 13);
                int createdYear = random.Next(2018, 2021);

                var dob = new DateTime(year, month, day);
                var createdDate = new DateTime(createdYear, createdMonth, createdDay);

                user.CreatedAt = createdDate;
                user.Dob = dob.Date;

                users.Add(user);
            }

            //var response = new List<UserResponse>();
            //foreach (var item in users)
            //{
            //    var u = new UserResponse(item, _storageService);
            //    response.Add(u);
            //}
            var fromDate = new DateTime(2018, 1, 1);
            var toDate = new DateTime(2020, 12, 30);
            List<Access> accesses = new List<Access>();

            while (toDate > fromDate)
            {
                var access = new Access();
                access.AuthorizeCount = random.Next(500, 2000);
                access.UnauthorizeCount = random.Next(200, 800);
                access.Date = fromDate.Date;
                fromDate = fromDate.AddDays(1);
                accesses.Add(access);
            }

            _context.Accesses.AddRange(accesses);
            _context.Users.AddRange(users);
            _context.SaveChanges();
            return Ok();
        }

        private static T RandomEnumValue<T>()
        {
            Random random = new Random();
            var values = Enum.GetValues(typeof(T));
            int num = random.Next(0, values.Length);
            return (T)values.GetValue(num);
        }

        [HttpGet("similar/{userId}")]
        public async Task<IActionResult> GetMatrix(Guid userId, [FromQuery] FilterUserViewModel filter)
        {
            var user = await _userApplication.GetById(userId);

            var tempUsers = await _context.Users
                .Where(x => x.Id != userId && x.Gender == user.FindPeople && x.Status == EUserStatus.Active && x.IsInfoUpdated)
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

            users.RemoveAt(0);

            for (int i = 1; i < kq.Count; i++)
            {
                users[i - 1].Point = kq[i];
            }

            users = users.OrderByDescending(o => o.Point).ToList();

            var pageTotal = users.Count / filter.PageSize;

            users = users
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize).ToList();

            var usersDisplay = await _userApplication.GetUserDisplay(users);

            return Ok(new {
                data = usersDisplay,
                pageTotal = pageTotal
            });
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromForm] UserRequest request)
        {
            var user = await _userApplication.GetById(request.Id);
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
                user = await _userApplication.UpdateUser(user);
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

        [Authorize]
        [HttpPost("filterFeatures")]
        public async Task<IActionResult> FilterFeatures([FromQuery] PagingRequest pagingRequest, [FromBody] List<FilterFeaturesRequest> requests)
        {
            if(requests.Count == 0)
            {
                var usersNotFilter = await _context.Users
                    .Where(x => x.Status == EUserStatus.Active && x.IsInfoUpdated)
                    .ToListAsync();

                var pageCount = usersNotFilter.Count / pagingRequest.PageSize;

                usersNotFilter = usersNotFilter
                .Skip((pagingRequest.PageIndex - 1) * pagingRequest.PageSize)
                .Take(pagingRequest.PageSize).ToList();

                var displayUsers = await _userApplication.GetUserDisplay(usersNotFilter);

                return Ok(new
                {
                    data = displayUsers,
                    pageTotal = pageCount
                });

            }
            var data = await filterFeatures(requests);

            var pageTotal = data.Count / pagingRequest.PageSize;

            var users = data
            .Skip((pagingRequest.PageIndex - 1) * pagingRequest.PageSize)
            .Take(pagingRequest.PageSize).ToList();

            var response = await _userApplication.GetUserDisplay(users);

            return Ok(new
            {
                data = response,
                pageTotal = pageTotal
            });
        }

        private async Task<List<AppUser>> filterFeatures(List<FilterFeaturesRequest> requests)
        {
            List<EBody> bodies = new List<EBody>();
            List<EGender> genders = new List<EGender>();
            List<EEducation> educations = new List<EEducation>();
            List<EReligion> religions = new List<EReligion>();
            List<ECook> cooks = new List<ECook>();
            List<ELikeTechnology> likeTechnologies = new List<ELikeTechnology>();
            List<ELikePet> likePets = new List<ELikePet>();
            List<EPlaySport> playSports = new List<EPlaySport>();
            List<ETravel> travels = new List<ETravel>();
            List<EGame> games = new List<EGame>();
            List<EShopping> shoppings = new List<EShopping>();
            List<ELocation> locations = new List<ELocation>();
            List<ECharacter> characters = new List<ECharacter>();
            //
            List<EFavoriteMovie> favoriteMovies = new List<EFavoriteMovie>();
            List<EAtmosphereLike> atmosphereLikes = new List<EAtmosphereLike>();
            List<EDrinkBeer> drinkBeers = new List<EDrinkBeer>();
            List<ESmoking> smokings = new List<ESmoking>();
            List<EMarriage> marriages = new List<EMarriage>();
            List<EJob> jobs = new List<EJob>();
            List<EAgeGroup> ageGroups = new List<EAgeGroup>();

            foreach (FilterFeaturesRequest item in requests)
            {
                switch (item.feature)
                {
                    case "body":
                        if (!bodies.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EBody body))
                            {
                                bodies.Add(body);
                            }
                        }
                        break;

                    case "gender":
                        if (!genders.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EGender gender))
                            {
                                genders.Add(gender);
                            }
                        }
                        break;
                    case "education":
                        if (!educations.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EEducation education))
                            {
                                educations.Add(education);
                            }
                        }
                        break;
                    case "religion":
                        if (!religions.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EReligion religion))
                            {
                                religions.Add(religion);
                            }
                        }
                        break;
                    case "cook":
                        if (!cooks.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out ECook cook))
                            {
                                cooks.Add(cook);
                            }
                        }
                        break;
                    case "likeTechnology":
                        if (!likeTechnologies.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out ELikeTechnology likeTechnology))
                            {
                                likeTechnologies.Add(likeTechnology);
                            }
                        }
                        break;
                    case "likePet":
                        if (!likePets.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out ELikePet likePet))
                            {
                                likePets.Add(likePet);
                            }
                        }
                        break;
                    case "playSport":
                        if (!playSports.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EPlaySport playSport))
                            {
                                playSports.Add(playSport);
                            }
                        }
                        break;
                    case "travel":
                        if (!travels.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out ETravel travel))
                            {
                                travels.Add(travel);
                            }
                        }
                        break;
                    case "game":
                        if (!games.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EGame game))
                            {
                                games.Add(game);
                            }
                        }
                        break;
                    case "shopping":
                        if (!shoppings.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EShopping shopping))
                            {
                                shoppings.Add(shopping);
                            }
                        }
                        break;
                    case "location":
                        if (!locations.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out ELocation location))
                            {
                                locations.Add(location);
                            }
                        }
                        break;
                    case "character":
                        if (!characters.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out ECharacter character))
                            {
                                characters.Add(character);
                            }
                        }
                        break;

                        //
                    case "favoriteMovie":
                        if (!favoriteMovies.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EFavoriteMovie favoriteMovie))
                            {
                                favoriteMovies.Add(favoriteMovie);
                            }
                        }
                        break;
                    case "atmosphereLike":
                        if (!atmosphereLikes.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EAtmosphereLike atmosphereLike))
                            {
                                atmosphereLikes.Add(atmosphereLike);
                            }
                        }
                        break;
                    case "drinkBeer":
                        if (!drinkBeers.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EDrinkBeer drinkBeer))
                            {
                                drinkBeers.Add(drinkBeer);
                            }
                        }
                        break;
                    case "smoking":
                        if (!smokings.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out ESmoking smoking))
                            {
                                smokings.Add(smoking);
                            }
                        }
                        break;
                    case "marriage":
                        if (!marriages.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EMarriage marriage))
                            {
                                marriages.Add(marriage);
                            }
                        }
                        break;
                    case "job":
                        if (!jobs.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EJob job))
                            {
                                jobs.Add(job);
                            }
                        }
                        break;
                    case "ageGroup":
                        if (!ageGroups.Any(x => x.ToString() == item.display))
                        {
                            if (Enum.TryParse(item.display.Trim(), out EAgeGroup ageGroup))
                            {
                                ageGroups.Add(ageGroup);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }

            var users = await _context.Users.Where(x => x.Status == EUserStatus.Active && x.IsInfoUpdated).ToListAsync(); 
            List<AppUser> filterUsers = new List<AppUser>();
            //

            if (jobs.Count > 0)
            {
                foreach (EJob item in jobs)
                {
                    var filter = users.Where(x => x.Job == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            //


            if (marriages.Count > 0)
            {
                foreach (EMarriage item in marriages)
                {
                    var filter = users.Where(x => x.Marriage == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            //


            if (smokings.Count > 0)
            {
                foreach (ESmoking item in smokings)
                {
                    var filter = users.Where(x => x.Smoking == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            //

            if (drinkBeers.Count > 0)
            {
                foreach (EDrinkBeer item in drinkBeers)
                {
                    var filter = users.Where(x => x.DrinkBeer == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            //

            if (atmosphereLikes.Count > 0)
            {
                foreach (EAtmosphereLike item in atmosphereLikes)
                {
                    var filter = users.Where(x => x.AtmosphereLike == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            //

            if (favoriteMovies.Count > 0)
            {
                foreach (EFavoriteMovie item in favoriteMovies)
                {
                    var filter = users.Where(x => x.FavoriteMovie == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            //

            //
            if (bodies.Count > 0)
            {
                foreach (EBody item in bodies)
                {
                    var filter = users.Where(x => x.Body == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            //
            if(genders.Count > 0)
            {
                foreach (EGender item in genders)
                {
                    var filter = users.Where(x => x.Gender == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(educations.Count > 0)
            {
                foreach (EEducation item in educations)
                {
                    var filter = users.Where(x => x.Education == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(religions.Count > 0)
            {
                foreach (EReligion item in religions)
                {
                    var filter = users.Where(x => x.Religion == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(cooks.Count > 0)
            {
                foreach (ECook item in cooks)
                {
                    var filter = users.Where(x => x.Cook == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(likeTechnologies.Count > 0)
            {
                foreach (ELikeTechnology item in likeTechnologies)
                {
                    var filter = users.Where(x => x.LikeTechnology == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(likePets.Count > 0)
            {
                foreach (ELikePet item in likePets)
                {
                    var filter = users.Where(x => x.LikePet == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(playSports.Count > 0)
            {
                foreach (EPlaySport item in playSports)
                {
                    var filter = users.Where(x => x.PlaySport == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(travels.Count > 0)
            {
                foreach (ETravel item in travels)
                {
                    var filter = users.Where(x => x.Travel == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(games.Count > 0)
            {
                foreach (EGame item in games)
                {
                    var filter = users.Where(x => x.Game == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(shoppings.Count > 0)
            {
                foreach (EShopping item in shoppings)
                {
                    var filter = users.Where(x => x.Shopping == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if(locations.Count > 0)
            {
                foreach (ELocation item in locations)
                {
                    var filter = users.Where(x => x.Location == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if (characters.Count > 0)
            {
                foreach (ECharacter item in characters)
                {
                    var filter = users.Where(x => x.Character == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }

            //
            if (ageGroups.Count > 0)
            {
                foreach (EAgeGroup item in ageGroups)
                {
                    var filter = users.Where(x => _userApplication.GetAgeGroup(x.Dob) == item).ToList();
                    filterUsers = filterUsers.Concat(filter)
                                        .ToList();
                }
                users = filterUsers;
                filterUsers = new List<AppUser>();
            }
            return users;
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
                FindPeople.Add(item.ToString());
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

            var AgeGroup = new List<string>();
            List<EAgeGroup> ageGroups = Enum.GetValues(typeof(EAgeGroup))
                    .Cast<EAgeGroup>()
                    .ToList();
            foreach (var item in ageGroups)
            {
                AgeGroup.Add(item.ToString());
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
                UserStatus,
                AgeGroup
            };

            return Ok(response);
        }
    }
}