﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServe.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime? DoB { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string IsActive { get; set; }
    }
    
    public class LoginUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime? DoB { get; set; }
        public string Role { get; set; }
    }
}
