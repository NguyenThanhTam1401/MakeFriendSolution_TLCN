﻿using MakeFriendSolution.Models;
using MakeFriendSolution.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.EF
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            var adminId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C96D");
            var vuongId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C96E");
            builder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = adminId,
                    UserName = "Admin",
                    FullName = "Nguyễn Thành Tâm",
                    Email = "tam@gmail.com",
                    PassWord = "admin",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "Tam.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = "Tìm người yêu",
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
                    IAm = EIAm.Nam_Tìm_Nữ,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Lạc_Quan_Yêu_Đời,
                    Marriage = EMarriage.Đang_Yêu,
                    MostValuable = EMostValuable.Bạn_Bè,
                    Religion = EReligion.Không_Có_Đạo,
                    Smoking = ESmoking.Chỉ_Hút_Xã_Giao,
                    Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Bạn_Đời,
                    Title = "Thông tin của tôi",
                    AtmosphereLike = EAtmosphereLike.Vui_Tươi,
                    FavoriteMovie = EFavoriteMovie.Hoạt_Hình,
                    IsInfoUpdated = 1
                },
                new AppUser()
                {
                    Id = Guid.NewGuid(),
                    UserName = "tamxix",
                    FullName = "Nguyễn Thành Tâm",
                    Email = "ng.th.tam1401@gmail.com",
                    PassWord = "123",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "Tam.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = "Tìm người yêu",
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
                    IAm = EIAm.Nam_Tìm_Nữ,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Lạc_Quan_Yêu_Đời,
                    Marriage = EMarriage.Đang_Yêu,
                    MostValuable = EMostValuable.Bạn_Bè,
                    Religion = EReligion.Không_Có_Đạo,
                    Smoking = ESmoking.Chỉ_Hút_Xã_Giao,
                    Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Bạn_Đời,
                    Title = "Thông tin của tôi",
                    AtmosphereLike = EAtmosphereLike.Vui_Tươi,
                    FavoriteMovie = EFavoriteMovie.Hoạt_Hình,
                    IsInfoUpdated = 1
                },

                new AppUser()
                {
                    Id = Guid.NewGuid(),
                    UserName = "HuyenNhung",
                    FullName = "Nguyễn Huyền Nhung",
                    Email = "nhung@gmail.com",
                    PassWord = "admin",
                    PhoneNumber = "0369875463",
                    Role = ERole.User,
                    Status = EUserStatus.Active,
                    AvatarPath = "Tam.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nữ,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,

                    FindPeople = "Tìm người yêu",
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
                    IAm = EIAm.Nữ_Tìm_Nam,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Lãng_mạn,
                    Marriage = EMarriage.Đang_Yêu,
                    MostValuable = EMostValuable.Bạn_Đời,
                    Religion = EReligion.Không_Có_Đạo,
                    Smoking = ESmoking.Chỉ_Hút_Xã_Giao,
                    Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Bạn_Đời,
                    Title = "Thông tin của tôi",
                    AtmosphereLike = EAtmosphereLike.Vui_Tươi,
                    FavoriteMovie = EFavoriteMovie.Hoạt_Hình,
                    IsInfoUpdated = 1
                },

                new AppUser()
                {
                    Id = vuongId,
                    UserName = "vuong",
                    FullName = "Nguyên Vương",
                    Email = "vuong@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "vuong.jpg",
                    Location = ELocation.Hà_Nội,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = "Tìm người thương",
                    Body = EBody.Mũm_Mĩm,
                    Character = ECharacter.Dịu_Dàng,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Học,
                    IAm = EIAm.Nam_Tìm_Nữ,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Giản_Dị,
                    Marriage = EMarriage.Ly_Dị,
                    MostValuable = EMostValuable.Bạn_Bè,
                    Religion = EReligion.Đạo_Phật,
                    Smoking = ESmoking.Chỉ_Hút_Xã_Giao,
                    Summary = "Tôi là Vương, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Người_Yêu_Lâu_Dài,
                    Title = "Thông tin của tôi",
                    AtmosphereLike = EAtmosphereLike.Tĩnh_Lặng,
                    FavoriteMovie = EFavoriteMovie.Hài_Hước,
                    IsInfoUpdated = 1
                }
            );
        }
    }
}