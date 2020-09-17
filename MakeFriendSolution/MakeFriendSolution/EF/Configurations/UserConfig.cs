using MakeFriendSolution.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.EF.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.PassWord).IsRequired().IsUnicode(false).HasMaxLength(200);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IsInfoUpdated).HasDefaultValue(0);

            builder.HasMany(x => x.SendMessages).WithOne(a => a.Sender).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ReceiveMessages).WithOne(a => a.Receiver).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}