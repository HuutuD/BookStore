﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopBusiness
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Navigation properties
        public ICollection<Shipping> SubmittedShippings { get; set; }
        public ICollection<Shipping> ApprovedShippings { get; set; }
    }
}
