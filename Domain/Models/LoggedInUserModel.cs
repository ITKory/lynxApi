﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LoggedInUserModel
    {
        public string Token { get; set; }
        public User User { get; set; }
        public string[] Roles { get; set; }
    
    }
}
