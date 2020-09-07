using MakeFriendSolution.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models
{
    public class Profile
    {
        public Profile()
        {
        }

        public Profile(Profile profile)
        {
            Id = profile.Id;
            Title = profile.Title;
            Summary = profile.Summary;
            FindPeople = profile.FindPeople;
            Weight = profile.Weight;
            Height = profile.Height;
            Dob = profile.Dob;
            UserId = profile.UserId;
            IAm = profile.IAm;
            Marriage = profile.Marriage;
            Target = profile.Target;
            Education = profile.Education;
            Body = profile.Body;
            Character = profile.Character;
            LifeStyle = profile.LifeStyle;
            MostValuable = profile.MostValuable;
            Job = profile.Job;
            Religion = profile.Religion;
            Smoking = profile.Smoking;
            DrinkBeer = profile.DrinkBeer;
            Children = profile.Children;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string FindPeople { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Dob { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

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
        public ESmoking Smoking { get; set; }
        public EDrinkBeer DrinkBeer { get; set; }
        public EChildren Children { get; set; }
    }
}