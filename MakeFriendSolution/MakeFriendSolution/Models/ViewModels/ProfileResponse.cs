using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class ProfileResponse
    {
        public ProfileResponse(AppUser profile)
        {
            Title = profile.Title;
            Summary = profile.Summary;
            FindPeople = profile.FindPeople;
            Weight = profile.Weight;
            Height = profile.Height;
            Dob = profile.Dob;
            IAm = profile.IAm.ToString();
            Marriage = profile.Marriage.ToString();
            Target = profile.Target.ToString();
            Education = profile.Education.ToString();
            Body = profile.Body.ToString();
            Character = profile.Character.ToString();
            LifeStyle = profile.LifeStyle.ToString();
            MostValuable = profile.MostValuable.ToString();
            Job = profile.Job.ToString();
            Religion = profile.Religion.ToString();
            Smoking = profile.Smoking.ToString();
            DrinkBeer = profile.DrinkBeer.ToString();
            Children = profile.Children.ToString();
            FavoriteMovie = profile.FavoriteMovie.ToString();
            AtmosphereLike = profile.AtmosphereLike.ToString();
        }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string FindPeople { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Dob { get; set; }

        /// <summary>
        /// Dưới đây là các thông số dùng để tính toán
        /// </summary>

        public string IAm { get; set; }
        public string Marriage { get; set; }
        public string Target { get; set; }
        public string Education { get; set; }
        public string Body { get; set; }
        public string Character { get; set; }
        public string LifeStyle { get; set; }
        public string MostValuable { get; set; }
        public string Job { get; set; }
        public string Religion { get; set; }
        public string FavoriteMovie { get; set; }
        public string AtmosphereLike { get; set; }
        public string Smoking { get; set; }
        public string DrinkBeer { get; set; }
        public string Children { get; set; }
    }
}