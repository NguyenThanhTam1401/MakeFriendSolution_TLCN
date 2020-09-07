using MakeFriendSolution.Models;
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
            builder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
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
                    UpdatedAt = new DateTime(2020, 09, 07)
                },
                new User()
                {
                    Id = 2,
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
                    UpdatedAt = new DateTime(2020, 09, 07)
                }
            );

            builder.Entity<Profile>().HasData(
                new Profile()
                {
                    Id = 1,
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = "Tìm người yêu",
                    Body = EBody.Cân_Đối,
                    Character = ECharacter.Chung_Thủy,
                    Children = EChildren.Chưa_Có,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Đẳng,
                    IAm = EIAm.Nam_Tìm_Nữ,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Gần_Gũi_Chan_Hòa_Với_Thiên_Nhiên,
                    Marriage = EMarriage.Đang_Yêu,
                    MostValuable = EMostValuable.Bạn_Tâm_Giao_Tri_Kỷ,
                    Religion = EReligion.Không_Có_Đạo,
                    Smoking = ESmoking.Chỉ_Hút_Xã_Giao,
                    Summary = "Tôi là Tâm, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Bạn_Đời,
                    Title = "Thông tin của tôi",
                    UserId = 1
                },
                new Profile()
                {
                    Id = 2,
                    Dob = new DateTime(1999, 01, 14),
                    Height = 170,
                    Weight = 65,
                    FindPeople = "Tìm người thương",
                    Body = EBody.Mũm_Mĩm,
                    Character = ECharacter.Dịu_Dàng,
                    Children = EChildren.Đã_Có_Nhưng_Không_Sống_Chung,
                    DrinkBeer = EDrinkBeer.Uống_Khá_Nhiều,
                    Education = EEducation.Cao_Học,
                    IAm = EIAm.Nam_Tìm_Nữ,
                    Job = EJob.IT_Công_Nghệ_Thông_Tin,
                    LifeStyle = ELifeStyle.Bình_Dân,
                    Marriage = EMarriage.Ly_Dị,
                    MostValuable = EMostValuable.Bản_Thân_Mình,
                    Religion = EReligion.Đạo_Phật,
                    Smoking = ESmoking.Chỉ_Hút_Xã_Giao,
                    Summary = "Tôi là Vương, rất vui khi được làm quen với bạn",
                    Target = ETarget.Tìm_Người_Yêu_Lâu_Dài,
                    Title = "Thông tin của tôi",
                    UserId = 2
                }
            );
        }
    }
}