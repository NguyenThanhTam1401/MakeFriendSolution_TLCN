using MakeFriendSolution.EF;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MakeFriendSolution.Middlewares
{
    public class AccessMiddleware : IMiddleware
    {
        private readonly MakeFriendDbContext _context;

        public AccessMiddleware(MakeFriendDbContext context)
        {
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var access = _context.Accesses.FirstOrDefault();
            var identity = context.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();

            if (claims.Count == 0)
                access.UnauthorizeCount += 1;
            else
                access.AuthorizeCount += 1;

            _context.Accesses.Update(access);
            await _context.SaveChangesAsync();
            await next(context);
        }
    }
}