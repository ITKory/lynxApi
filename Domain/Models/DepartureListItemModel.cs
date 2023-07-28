﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DepartureListItemModel
    {
        public   string Title { get; set; }
        public    int Id { get; set; }
        public DateOnly BDay { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
        public bool IsFound { get; set; }
    }
}
