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
        public async Task<IActionResult> GetMatrix(int userId)
        {
            var usersResponse = new List<UserResponse>();

            var user = await _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.Profile)
                .FirstOrDefaultAsync();

            UserResponse mainUser = new UserResponse(user);
            usersResponse.Add(mainUser);

            var users = await _context.Users
                .Include(x => x.Profile)
                .Where(x => x.Id != userId)
                .ToListAsync();

            users.Insert(0, user);

            foreach (var item in users)
            {
                UserResponse userResponse = new UserResponse(item);
                usersResponse.Add(userResponse);
            }

            int sl = users.Count;

            double[,] usersMatrix = new double[sl, 13];
            for (int i = 0; i < sl; i++)
            {
                usersMatrix[i, 0] = (double)users[i].Profile.IAm;
                usersMatrix[i, 1] = (double)users[i].Profile.Marriage;
                usersMatrix[i, 2] = (double)users[i].Profile.Target;
                usersMatrix[i, 3] = (double)users[i].Profile.Education;
                usersMatrix[i, 4] = (double)users[i].Profile.Body;
                usersMatrix[i, 5] = (double)users[i].Profile.Character;
                usersMatrix[i, 6] = (double)users[i].Profile.LifeStyle;
                usersMatrix[i, 7] = (double)users[i].Profile.MostValuable;
                usersMatrix[i, 8] = (double)users[i].Profile.Job;
                usersMatrix[i, 9] = (double)users[i].Profile.Religion;
                usersMatrix[i, 10] = (double)users[i].Profile.Smoking;
                usersMatrix[i, 11] = (double)users[i].Profile.DrinkBeer;
                usersMatrix[i, 12] = (double)users[i].Profile.Children;
            }

            cMatrix m = new cMatrix();
            m.Row = sl;
            m.Column = 13;
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

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _context.Profiles.ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, Profile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Profiles
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Profile>> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return profile;
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}