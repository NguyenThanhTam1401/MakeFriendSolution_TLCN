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
            var hieuId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C96F");
            var dinhId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C961");
            var datId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C962");
            var sonId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C963");
            var ducId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C964");
            var tienId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C965");
            var nhungId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C966");
            var nhung2Id = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C967");
            var diemId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C968");
            var hanId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C969");
            var maiId = new Guid("EC826AF8-0310-48CF-8A14-DA11BDB1C977");
            builder.Entity<SimilarityFeature>().HasData(
                new SimilarityFeature()
                {
                    UpdatedAt = DateTime.Now,
                    Id = 1
                }
            );

            builder.Entity<AppUser>().HasData(
                //Tam
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
                    AvatarPath = "tam.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nữ,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Dễ_Gần,
                    DrinkBeer = EDrinkBeer.Uống_Nhiều,
                    Education = EEducation.Đại_Học,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Năng_Động,
                    Marriage = EMarriage.Đã_Kết_Hôn,
                    MostValuable = EMostValuable.Gia_Đình,
                    Religion = EReligion.Đạo_Thiên_Chúa,
                    Smoking = ESmoking.Không_Hút_Thuốc,
                    Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Bạn_Đời,
                    Title = "Thông tin của tôi",
                    AtmosphereLike = EAtmosphereLike.Náo_Nhiệt,
                    FavoriteMovie = EFavoriteMovie.Giật_Gân,
                    IsInfoUpdated = true
                },
                //Nhung2
                new AppUser()
                {
                    Id = nhung2Id,
                    UserName = "nhung2",
                    FullName = "Nguyễn Huyền Nhung",
                    Email = "nhung@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0369875463",
                    Role = ERole.User,
                    Status = EUserStatus.Active,
                    AvatarPath = "nhung2.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nữ,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,

                    FindPeople = EGender.Nam,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Năng_Động,
                    Marriage = EMarriage.Đang_Yêu,
                    MostValuable = EMostValuable.Bạn_Đời,
                    Religion = EReligion.Không_Có_Đạo,
                    Smoking = ESmoking.Chỉ_Hút_Xã_Giao,
                    Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Bạn_Đời,
                    Title = "Thông tin của tôi",
                    AtmosphereLike = EAtmosphereLike.Vui_Tươi,
                    FavoriteMovie = EFavoriteMovie.Hoạt_Hình,
                    IsInfoUpdated = true
                },
                //Vuong
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
                    FindPeople = EGender.Nữ,
                    Body = EBody.Mũm_Mĩm,
                    Character = ECharacter.Dịu_Dàng,
                    DrinkBeer = EDrinkBeer.Chỉ_Uống_Xã_Giao,
                    Education = EEducation.Cao_Học,
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
                    IsInfoUpdated = true
                },
                //hieu
                new AppUser()
                {
                    Id = hieuId,
                    UserName = "hieu",
                    FullName = "Võ Minh Hiếu",
                    Email = "hieu@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "hieu.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nữ,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Tien
                new AppUser()
                {
                    Id = tienId,
                    UserName = "tien",
                    FullName = "Lê Minh Tiến",
                    Email = "tien@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "tien.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nữ,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Dinh
                new AppUser()
                {
                    Id = dinhId,
                    UserName = "Dinh",
                    FullName = "Lê Kim Đỉnh",
                    Email = "dinh@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "dinh.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nữ,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Dat
                new AppUser()
                {
                    Id = datId,
                    UserName = "Dat",
                    FullName = "Hồ Quốc Đạt",
                    Email = "dat@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "dat.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nữ,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Son
                new AppUser()
                {
                    Id = sonId,
                    UserName = "Son",
                    FullName = "Phan Sơn",
                    Email = "son@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "son.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nữ,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Duc
                new AppUser()
                {
                    Id = ducId,
                    UserName = "Duc",
                    FullName = "Trí Đức",
                    Email = "duc@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "duc.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nam,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nữ,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Nhung
                new AppUser()
                {
                    Id = nhungId,
                    UserName = "GiaNhung",
                    FullName = "Gia Nhung",
                    Email = "nhung1@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "nhung.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nữ,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nam,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Diem
                new AppUser()
                {
                    Id = diemId,
                    UserName = "Diem",
                    FullName = "Kiều Diễm",
                    Email = "diem@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "diem.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nữ,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nam,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Han
                new AppUser()
                {
                    Id = hanId,
                    UserName = "Han",
                    FullName = "Gia Hân",
                    Email = "han@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "han.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nữ,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nam,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                },
                //Mai
                new AppUser()
                {
                    Id = maiId,
                    UserName = "Mai",
                    FullName = "Xuân Maiii",
                    Email = "mai@gmail.com",
                    PassWord = "1111",
                    PhoneNumber = "0396925225",
                    Role = ERole.Admin,
                    Status = EUserStatus.Active,
                    AvatarPath = "mai.jpg",
                    Location = ELocation.TP_HCM,
                    Gender = EGender.Nữ,
                    CreatedAt = new DateTime(2020, 09, 07),
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = EGender.Nam,
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
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
                    IsInfoUpdated = true
                }
            );
        }
    }
}