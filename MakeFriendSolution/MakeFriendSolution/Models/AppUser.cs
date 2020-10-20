using MakeFriendSolution.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
        public ERole Role { get; set; }
        public ETypeAccount TypeAccount { get; set; }
        public string FullName { get; set; }
        public EGender Gender { get; set; }
        public string AvatarPath { get; set; }
        public ELocation Location { get; set; }
        public EUserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public int IsInfoUpdated { get; set; }
        public string PasswordForgottenCode { get; set; }
        public DateTime PasswordForgottenPeriod { get; set; }
        public int NumberOfPasswordConfirmations { get; set; }

        //

        public string Title { get; set; }
        public string Summary { get; set; }
        public string FindPeople { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Dob { get; set; }

        /// <summary>
        /// Dưới đây là các thông số dùng để tính toán
        /// </summary>

        public EIAm IAm { get; set; }
        public EMarriage Marriage { get; set; }
        public ETarget Target { get; set; }
        public EEducation Education { get; set; }
        public EBody Body { get; set; }
        public ECharacter Character { get; set; }
        public ELifeStyle LifeStyle { get; set; }
        public EMostValuable MostValuable { get; set; }
        public EJob Job { get; set; }
        public EReligion Religion { get; set; }
        public EFavoriteMovie FavoriteMovie { get; set; }
        public EAtmosphereLike AtmosphereLike { get; set; }
        public ESmoking Smoking { get; set; }
        public EDrinkBeer DrinkBeer { get; set; }

        //

        public ICollection<ThumbnailImage> ThumbnailImages { get; set; }
        public ICollection<HaveMessage> SendMessages { get; set; }
        public ICollection<HaveMessage> ReceiveMessages { get; set; }
        public ICollection<Follow> Followed { get; set; }
        public ICollection<Follow> BeingFollowedBy { get; set; }
        public ICollection<Favorite> Favorited { get; set; }
        public ICollection<Favorite> BeingFavoritedBy { get; set; }
        public ICollection<BlockUser> UserWasBlock { get; set; }
        public ICollection<BlockUser> BlockedByUsers { get; set; }

        public AppUser(AppUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            PassWord = user.PassWord;
            Role = user.Role;
            FullName = user.FullName;
            Gender = user.Gender;
            AvatarPath = user.AvatarPath;
            Location = user.Location;
            Status = user.Status;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            CreatedAt = user.CreatedAt;

            //

            Title = user.Title;
            Summary = user.Summary;
            FindPeople = user.FindPeople;
            Weight = user.Weight;
            Height = user.Height;
            Dob = user.Dob;
            IAm = user.IAm;
            Marriage = user.Marriage;
            Target = user.Target;
            Education = user.Education;
            Body = user.Body;
            Character = user.Character;
            LifeStyle = user.LifeStyle;
            MostValuable = user.MostValuable;
            Job = user.Job;
            Religion = user.Religion;
            Smoking = user.Smoking;
            DrinkBeer = user.DrinkBeer;
        }

        public AppUser()
        {
        }
    }
}