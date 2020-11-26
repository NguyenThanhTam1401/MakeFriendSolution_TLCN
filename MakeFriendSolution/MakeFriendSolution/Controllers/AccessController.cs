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

            var listAccess = byMonth.Item2;
            listAccess = listAccess.OrderBy(x => x.Period).ToList();

            return Ok(new
            {
                accessTotal = byMonth.Item1,
                listAccess = listAccess
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


            if(datetime.Month == 2)
            {
                for (int i = 1; i <= 28; i++)
                {
                    var access = new AccessResponse();
                    var date = new DateTime(datetime.Year, datetime.Month, i);
                    access.Period = date;

                    var item = accessCount.Where(x => x.Date.Day == i).FirstOrDefault();

                    if (item != null)
                    {
                        accessTotal.AuthorizeCount += item.AuthorizeCount;
                        accessTotal.UnauthorizeCount += item.UnauthorizeCount;
                        access.AuthorizeCount = item.AuthorizeCount;
                        access.UnauthorizeCount = item.UnauthorizeCount;
                        access.Total = access.AuthorizeCount + access.UnauthorizeCount;
                    }

                    accessResponse.Add(access);
                }

                var access2 = new AccessResponse();
                var date2 = new DateTime(datetime.Year, 3, 29);
                access2.Period = date2;
                accessResponse.Add(access2);

                access2 = new AccessResponse();
                date2 = new DateTime(datetime.Year, 3, 30);
                access2.Period = date2;
                accessResponse.Add(access2);

            }
            else
            {
                for (int i = 1; i <= 30; i++)
                {
                    var access = new AccessResponse();
                    var date = new DateTime(datetime.Year, datetime.Month, i);
                    access.Period = date;

                    var item = accessCount.Where(x => x.Date.Day == i).FirstOrDefault();

                    if (item != null)
                    {
                        accessTotal.AuthorizeCount += item.AuthorizeCount;
                        accessTotal.UnauthorizeCount += item.UnauthorizeCount;
                        access.AuthorizeCount = item.AuthorizeCount;
                        access.UnauthorizeCount = item.UnauthorizeCount;
                        access.Total = access.AuthorizeCount + access.UnauthorizeCount;
                    }

                    accessResponse.Add(access);
                }
            }

            accessTotal.Total = accessTotal.AuthorizeCount + accessTotal.UnauthorizeCount;
            accessTotal.Period = datetime;

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
                accessTotal.Period = byMonth.Item1.Period;

                accessCount.Add(byMonth.Item1);
            }

            accessTotal.Total = accessTotal.UnauthorizeCount + accessTotal.UnauthorizeCount;
            accessTotal.Period = dateTime;

            return (accessTotal, accessCount);
        }

        [HttpGet("getTheNumberOfNewUsersByMonth")]
        public async Task<IActionResult> GetTheNumberOfNewUsersByMonth()
        {
            var numberOfUsers = await _context.Users
                .Where(x => x.CreatedAt.Year == DateTime.Now.Year && x.CreatedAt.Month == DateTime.Now.Month)
                .CountAsync();

            int numberOfLastMonth = await _context.Users
                .Where(x => x.CreatedAt.Year == DateTime.Now.AddMonths(-1).Year && x.CreatedAt.Month == DateTime.Now.AddMonths(-1).Month)
                .CountAsync();

            double percents = Math.Round(((double)(numberOfUsers - numberOfLastMonth) / numberOfUsers  * 100), 2);

            return Ok(new
            {
                thisMonth = numberOfUsers,
                lastMonth = numberOfLastMonth,
                growthRate = percents
            });
        }

        [HttpGet("getNumberOfActiveUsers")]
        public async Task<IActionResult> GetNumberOfActiveUsers()
        {
            var numberOfActiveUsers = await _context.Users.Where(x => x.Status == Models.Enum.EUserStatus.Active).CountAsync();
            var numberOfInactiveUsers = await _context.Users.Where(x => x.Status != Models.Enum.EUserStatus.Active).CountAsync();
            return Ok(new { 
                activeAccounts = numberOfActiveUsers,
                inactiveAccounts = numberOfInactiveUsers
            });
        }
    }
}