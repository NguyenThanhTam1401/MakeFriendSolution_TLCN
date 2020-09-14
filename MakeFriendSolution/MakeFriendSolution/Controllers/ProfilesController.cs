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

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;

        public ProfilesController(MakeFriendDbContext context)
        {
            _context = context;
        }

        [HttpGet("matrix/{userId}")]
        public async Task<IActionResult> GetMatrix(string userId, [FromQuery] FilterUserViewModel filter)
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
                UserResponse userResponse = new UserResponse(item);
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
                usersMatrix[i, 4] = (double)users[i].Character;
                usersMatrix[i, 5] = (double)users[i].LifeStyle;
                usersMatrix[i, 6] = (double)users[i].MostValuable;
                usersMatrix[i, 7] = (double)users[i].Job;
                usersMatrix[i, 8] = (double)users[i].Religion;
                usersMatrix[i, 9] = (double)users[i].Smoking;
                usersMatrix[i, 10] = (double)users[i].DrinkBeer;
                usersMatrix[i, 11] = (double)users[i].Children;
            }

            cMatrix m = new cMatrix();
            m.Row = sl;
            m.Column = 12;
            m.Matrix = usersMatrix;

            List<double> kq = new List<double>();
            kq = m.TinhCos();

            for (int i = 0; i < kq.Count; i++)
            {
                usersResponse[i].Point = kq[i];
            }

            usersResponse.RemoveAt(0);
            usersResponse = usersResponse.OrderByDescending(o => o.Point).ToList();
            return Ok(usersResponse);
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