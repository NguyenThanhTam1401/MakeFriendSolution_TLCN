using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models.ViewModels
{
    public class UserRequest
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public IFormFile AvatarFile { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }

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