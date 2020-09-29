﻿// <auto-generated />
using System;
using MakeFriendSolution.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MakeFriendSolution.Migrations
{
    [DbContext(typeof(MakeFriendDbContext))]
    [Migration("20200928120009_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MakeFriendSolution.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AtmosphereLike")
                        .HasColumnType("int");

                    b.Property<string>("AvatarPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Body")
                        .HasColumnType("int");

                    b.Property<int>("Character")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<int>("DrinkBeer")
                        .HasColumnType("int");

                    b.Property<int>("Education")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("FavoriteMovie")
                        .HasColumnType("int");

                    b.Property<string>("FindPeople")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("IAm")
                        .HasColumnType("int");

                    b.Property<int>("IsInfoUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Job")
                        .HasColumnType("int");

                    b.Property<int>("LifeStyle")
                        .HasColumnType("int");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<int>("Marriage")
                        .HasColumnType("int");

                    b.Property<int>("MostValuable")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPasswordConfirmations")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("PasswordForgottenCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<DateTime>("PasswordForgottenPeriod")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Religion")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Smoking")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Target")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96d"),
                            AtmosphereLike = 3,
                            AvatarPath = "Tam.jpg",
                            Body = 2,
                            Character = 1,
                            CreatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dob = new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrinkBeer = 4,
                            Education = 2,
                            Email = "tam@gmail.com",
                            FavoriteMovie = 11,
                            FindPeople = "Tìm người yêu",
                            FullName = "Nguyễn Thành Tâm",
                            Gender = 0,
                            Height = 170,
                            IAm = 0,
                            IsInfoUpdated = 1,
                            Job = 7,
                            LifeStyle = 3,
                            Location = 37,
                            Marriage = 1,
                            MostValuable = 2,
                            NumberOfPasswordConfirmations = 0,
                            PassWord = "admin",
                            PasswordForgottenPeriod = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "0396925225",
                            Religion = 0,
                            Role = 0,
                            Smoking = 1,
                            Status = 0,
                            Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                            Target = 4,
                            Title = "Thông tin của tôi",
                            TypeAccount = 0,
                            UserName = "Admin",
                            Weight = 65
                        },
                        new
                        {
                            Id = new Guid("afa5c352-fd48-4172-83b7-40fb8f664f9f"),
                            AtmosphereLike = 3,
                            AvatarPath = "Tam.jpg",
                            Body = 2,
                            Character = 1,
                            CreatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dob = new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrinkBeer = 4,
                            Education = 2,
                            Email = "ng.th.tam1401@gmail.com",
                            FavoriteMovie = 11,
                            FindPeople = "Tìm người yêu",
                            FullName = "Nguyễn Thành Tâm",
                            Gender = 0,
                            Height = 170,
                            IAm = 0,
                            IsInfoUpdated = 1,
                            Job = 7,
                            LifeStyle = 3,
                            Location = 37,
                            Marriage = 1,
                            MostValuable = 2,
                            NumberOfPasswordConfirmations = 0,
                            PassWord = "123",
                            PasswordForgottenPeriod = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "0396925225",
                            Religion = 0,
                            Role = 0,
                            Smoking = 1,
                            Status = 0,
                            Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                            Target = 4,
                            Title = "Thông tin của tôi",
                            TypeAccount = 0,
                            UserName = "tamxix",
                            Weight = 65
                        },
                        new
                        {
                            Id = new Guid("275aea40-0189-4f1b-9058-2ca10f4455e3"),
                            AtmosphereLike = 3,
                            AvatarPath = "Tam.jpg",
                            Body = 2,
                            Character = 1,
                            CreatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dob = new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrinkBeer = 4,
                            Education = 2,
                            Email = "nhung@gmail.com",
                            FavoriteMovie = 11,
                            FindPeople = "Tìm người yêu",
                            FullName = "Nguyễn Huyền Nhung",
                            Gender = 1,
                            Height = 170,
                            IAm = 1,
                            IsInfoUpdated = 1,
                            Job = 7,
                            LifeStyle = 5,
                            Location = 37,
                            Marriage = 1,
                            MostValuable = 3,
                            NumberOfPasswordConfirmations = 0,
                            PassWord = "admin",
                            PasswordForgottenPeriod = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "0369875463",
                            Religion = 0,
                            Role = 1,
                            Smoking = 1,
                            Status = 0,
                            Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                            Target = 4,
                            Title = "Thông tin của tôi",
                            TypeAccount = 0,
                            UserName = "HuyenNhung",
                            Weight = 65
                        },
                        new
                        {
                            Id = new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96e"),
                            AtmosphereLike = 0,
                            AvatarPath = "vuong.jpg",
                            Body = 3,
                            Character = 3,
                            CreatedAt = new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dob = new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrinkBeer = 4,
                            Education = 4,
                            Email = "vuong@gmail.com",
                            FavoriteMovie = 7,
                            FindPeople = "Tìm người thương",
                            FullName = "Nguyên Vương",
                            Gender = 0,
                            Height = 170,
                            IAm = 0,
                            IsInfoUpdated = 1,
                            Job = 7,
                            LifeStyle = 1,
                            Location = 38,
                            Marriage = 2,
                            MostValuable = 2,
                            NumberOfPasswordConfirmations = 0,
                            PassWord = "1111",
                            PasswordForgottenPeriod = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "0396925225",
                            Religion = 2,
                            Role = 0,
                            Smoking = 1,
                            Status = 0,
                            Summary = "Tôi là Vương, rất vui khi được làm quen với bạn",
                            Target = 3,
                            Title = "Thông tin của tôi",
                            TypeAccount = 0,
                            UserName = "vuong",
                            Weight = 65
                        });
                });

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

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("HaveMessages");
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ThumbnailImages");
                });

            modelBuilder.Entity("MakeFriendSolution.Models.HaveMessage", b =>
                {
                    b.HasOne("MakeFriendSolution.Models.AppUser", "Receiver")
                        .WithMany("ReceiveMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MakeFriendSolution.Models.AppUser", "Sender")
                        .WithMany("SendMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MakeFriendSolution.Models.ThumbnailImage", b =>
                {
                    b.HasOne("MakeFriendSolution.Models.AppUser", "User")
                        .WithMany("ThumbnailImages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}