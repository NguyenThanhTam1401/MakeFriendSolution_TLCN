using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private ISessionService _sessionService;

        public AccessController(MakeFriendDbContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        [Authorize]
        [HttpGet("byDate")]
        public async Task<IActionResult> GetAccessCountByDate(DateTime Date)
        {
            if (Date.Date > DateTime.Now.Date)
            {
                return BadRequest(new
                {
                    Message = "Date is not valid"
                });
            }
            var accessCount = await _context.Accesses.Where(x => x.Date.Date == Date.Date).FirstOrDefaultAsync();

            return Ok(accessCount);
        }

        [Authorize]
        [HttpGet("byMonth")]
        public async Task<IActionResult> GetAccessCountByMonth(DateTime dateTime)
        {
            if (dateTime.Date > DateTime.Now.Date)
            {
                return BadRequest(new
                {
                    Message = "Date is not valid"
                });
            }

            var byMonth = await this.GetByMonth(dateTime);

            return Ok(new
            {
                accessTotal = byMonth.Item1,
                listAccess = byMonth.Item2
            });
        }

        [Authorize]
        [HttpGet("byYear")]
        public async Task<IActionResult> GetAccessCountByYear(DateTime dateTime)
        {
            if (dateTime.Date > DateTime.Now.Date)
            {
                return BadRequest(new
                {
                    Message = "Date is not valid"
                });
            }

            var byYear = await this.GetByYear(dateTime);

            return Ok(new
            {
                accessTotal = byYear.Item1,
                listAccess = byYear.Item2
            });
        }

        public async Task<(AccessResponse, List<AccessResponse>)> GetByMonth(DateTime datetime)
        {
            var accessCount = await _context.Accesses
            .Where(x => x.Date.Date.Year == datetime.Date.Year && x.Date.Date.Month == datetime.Date.Month)
            .ToListAsync();

            var accessResponse = new List<AccessResponse>();
            var accessTotal = new AccessResponse();
            foreach (var item in accessCount)
            {
                var access = new AccessResponse(item);
                accessTotal.AuthorizeCount += item.AuthorizeCount;
                accessTotal.UnauthorizeCount += item.UnauthorizeCount;

                accessResponse.Add(access);
            }

            accessTotal.Total = accessTotal.AuthorizeCount + accessTotal.UnauthorizeCount;

            return (accessTotal, accessResponse);
        }

        public async Task<(AccessResponse, List<AccessResponse>)> GetByYear(DateTime dateTime)
        {
            var accessCount = new List<AccessResponse>();
            var accessTotal = new AccessResponse();

            for (int i = 1; i <= 12; i++)
            {
                var date = new DateTime(dateTime.Year, i, 1);

                var byMonth = await GetByMonth(date);
                accessTotal.AuthorizeCount += byMonth.Item1.AuthorizeCount;
                accessTotal.UnauthorizeCount += byMonth.Item1.UnauthorizeCount;
                accessCount.Add(byMonth.Item1);
            }

            accessTotal.Total = accessTotal.UnauthorizeCount + accessTotal.UnauthorizeCount;
            accessTotal.Period = dateTime;

            return (accessTotal, accessCount);
        }
    }
}