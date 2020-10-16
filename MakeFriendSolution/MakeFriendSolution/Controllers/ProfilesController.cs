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

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;

        public ProfilesController(MakeFriendDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        [HttpGet("matrix/{userId}")]
        public async Task<IActionResult> GetMatrix(Guid userId, [FromQuery] FilterUserViewModel filter)
        {
            var usersResponse = new List<UserResponse>();

            var user = await _context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            var users = await _context.Users
                .Where(x => x.Id != userId && x.IAm != user.IAm)
                .ToListAsync();

            //FilterUsers
            FilterUers(ref users, filter);

            users.Insert(0, user);

            foreach (var item in users)
            {
                UserResponse userResponse = new UserResponse(item, _storageService);
                usersResponse.Add(userResponse);
            }

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

            for (int i = 0; i < kq.Count; i++)
            {
                usersResponse[i].Point = kq[i];
            }

            //usersResponse.RemoveAt(0);
            usersResponse = usersResponse.OrderByDescending(o => o.Point).ToList();
            return Ok(usersResponse);
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

            if (Enum.TryParse(request.IAm, out EIAm iAm))
            {
                user.IAm = iAm;
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

            if (request.FindPeople != "" && request.FindPeople != null)
            {
                user.FindPeople = request.FindPeople;
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
    }
}