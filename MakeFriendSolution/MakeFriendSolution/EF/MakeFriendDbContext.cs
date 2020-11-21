using MakeFriendSolution.EF.Configurations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HaveMessageConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new ImageConfig());
            modelBuilder.ApplyConfiguration(new LikeImageConfig());
            modelBuilder.ApplyConfiguration(new FollowConfig());
            modelBuilder.ApplyConfiguration(new FavoriteConfig());
            modelBuilder.ApplyConfiguration(new BlockUserConfig());
            modelBuilder.ApplyConfiguration(new AccessConfig());
            modelBuilder.Seed();
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<ThumbnailImage> ThumbnailImages { get; set; }
        public DbSet<HaveMessage> HaveMessages { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<LikeImage> LikeImages { get; set; }
        public DbSet<BlockUser> BlockUsers { get; set; }
        public DbSet<Access> Accesses { get; set; }
    }
}