using MakeFriendSolution.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.EF
{
    public class MakeFriendDbContext : DbContext
    {
        public MakeFriendDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}