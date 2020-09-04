using MakeFriendSolution.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.EF.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.PassWord).IsRequired().IsUnicode(false).HasMaxLength(200);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);

            builder.HasOne<Profile>(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<Profile>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}