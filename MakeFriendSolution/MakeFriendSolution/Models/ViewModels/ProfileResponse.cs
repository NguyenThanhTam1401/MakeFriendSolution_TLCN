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
            Job = profile.Job.ToString();
            Location = profile.Location.ToString();
            Title = profile.Title;
            FindPeople = profile.FindPeople.ToString();
            Weight = profile.Weight;
            Height = profile.Height;
            Dob = profile.Dob;
            Marriage = profile.Marriage.ToString();
            Target = profile.Target.ToString();
            Education = profile.Education.ToString();
            Body = profile.Body.ToString();
            Character = profile.Character.ToString();
            LifeStyle = profile.LifeStyle.ToString();
            MostValuable = profile.MostValuable.ToString();
            Religion = profile.Religion.ToString();
            Smoking = profile.Smoking.ToString();
            DrinkBeer = profile.DrinkBeer.ToString();
            FavoriteMovie = profile.FavoriteMovie.ToString();
            AtmosphereLike = profile.AtmosphereLike.ToString();

            Shopping = profile.Shopping.ToString();
            Travel = profile.Travel.ToString();
            Game = profile.Game.ToString();
            Cook = profile.Cook.ToString();
            LikeTechnology = profile.LikeTechnology.ToString();
            LikePet = profile.LikePet.ToString();
            PlaySport = profile.PlaySport.ToString();
    }

        public string Title { get; set; }
        public string FindPeople { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Dob { get; set; }
        public string Job { get; set; }
        public string Location { get; set; }

        /// <summary>
        /// Dưới đây là các thông số dùng để tính toán
        /// </summary>
        ///

        public string Marriage { get; set; }
        public string Target { get; set; }
        public string Education { get; set; }
        public string Body { get; set; }
        public string Character { get; set; }
        public string LifeStyle { get; set; }
        public string MostValuable { get; set; }
        public string Religion { get; set; }
        public string FavoriteMovie { get; set; }
        public string AtmosphereLike { get; set; }
        public string Smoking { get; set; }
        public string DrinkBeer { get; set; }

        //
        public string Shopping { get; set; }
        public string Travel { get; set; }
        public string Game { get; set; }
        public string Cook { get; set; }
        public string LikeTechnology { get; set; }
        public string LikePet { get; set; }
        public string PlaySport { get; set; }
    }
}