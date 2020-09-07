﻿// <auto-generated />
using System;
using MakeFriendSolution.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MakeFriendSolution.Migrations
{
    [DbContext(typeof(MakeFriendDbContext))]
    partial class MakeFriendDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MakeFriendSolution.Models.HaveMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(5000);

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("HaveMessages");
                });

            modelBuilder.Entity("MakeFriendSolution.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Body")
                        .HasColumnType("int");

                    b.Property<int>("Character")
                        .HasColumnType("int");

                    b.Property<int>("Children")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<int>("DrinkBeer")
                        .HasColumnType("int");

                    b.Property<int>("Education")
                        .HasColumnType("int");

                    b.Property<string>("FindPeople")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("IAm")
                        .HasColumnType("int");

                    b.Property<int>("Job")
                        .HasColumnType("int");

                    b.Property<int>("LifeStyle")
                        .HasColumnType("int");

                    b.Property<int>("Marriage")
                        .HasColumnType("int");

                    b.Property<int>("MostValuable")
                        .HasColumnType("int");

                    b.Property<int>("Religion")
                        .HasColumnType("int");

                    b.Property<int>("Smoking")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Target")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = 0,
                            Character = 1,
                            Children = 0,
                            Dob = new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrinkBeer = 4,
                            Education = 2,
                            FindPeople = "Tìm người yêu",
                            Height = 170,
                            IAm = 0,
                            Job = 7,
                            LifeStyle = 8,
                            Marriage = 3,
                            MostValuable = 23,
                            Religion = 0,
                            Smoking = 1,
                            Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                            Target = 5,
                            Title = "Thông tin của tôi",
                            UserId = 1,
                            Weight = 65
                        },
                        new
                        {
                            Id = 2,
                            Body = 3,
                            Character = 3,
                            Children = 2,
                            Dob = new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrinkBeer = 4,
                            Education = 4,
                            FindPeople = "Tìm người thương",
                            Height = 170,
                            IAm = 0,
                            Job = 7,
                            LifeStyle = 1,
                            Marriage = 1,
                            MostValuable = 1,
                            Religion = 2,
                            Smoking = 1,
                            Summary = "Tôi là Vương, rất vui khi được làm quen với bạn",
                            Target = 0,
                            Title = "Thông tin của tôi",
                            UserId = 2,
                            Weight = 65
                        });
                });

            modelBuilder.Entity("MakeFriendSolution.Models.ThumbnailImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Image title");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ThumbnailImages");
                });

            modelBuilder.Entity("MakeFriendSolution.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvatarPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvatarPath = "Tam.jpg",
                            CreatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "tam@gmail.com",
                            FullName = "Nguyễn Thành Tâm",
                            Gender = 0,
                            Location = 37,
                            PassWord = "admin",
                            PhoneNumber = "0396925225",
                            Role = 0,
                            Status = 0,
                            UpdatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            AvatarPath = "vuong.jpg",
                            CreatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vuong@gmail.com",
                            FullName = "Nguyên Vương",
                            Gender = 0,
                            Location = 38,
                            PassWord = "1111",
                            PhoneNumber = "0396925225",
                            Role = 0,
                            Status = 0,
                            UpdatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "vuong"
                        });
                });

            modelBuilder.Entity("MakeFriendSolution.Models.HaveMessage", b =>
                {
                    b.HasOne("MakeFriendSolution.Models.User", "Receiver")
                        .WithMany("ReceiveMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MakeFriendSolution.Models.User", "Sender")
                        .WithMany("SendMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MakeFriendSolution.Models.Profile", b =>
                {
                    b.HasOne("MakeFriendSolution.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("MakeFriendSolution.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MakeFriendSolution.Models.ThumbnailImage", b =>
                {
                    b.HasOne("MakeFriendSolution.Models.User", "User")
                        .WithMany("ThumbnailImages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
