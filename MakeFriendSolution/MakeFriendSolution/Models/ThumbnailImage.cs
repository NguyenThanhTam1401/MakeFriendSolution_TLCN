﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Models
{
    public class ThumbnailImage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}