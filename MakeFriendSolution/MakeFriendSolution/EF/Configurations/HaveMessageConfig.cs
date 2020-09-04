﻿using MakeFriendSolution.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.EF.Configurations
{
    public class HaveMessageConfig : IEntityTypeConfiguration<HaveMessage>
    {
        public void Configure(EntityTypeBuilder<HaveMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Content).IsRequired().HasMaxLength(5000);
            builder.HasOne(x => x.Sender).WithMany(x => x.SendMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Receiver).WithMany(x => x.ReceiveMessages).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.Status).HasDefaultValue(1);
        }
    }
}