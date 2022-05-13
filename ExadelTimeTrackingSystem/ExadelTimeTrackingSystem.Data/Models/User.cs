using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelTimeTrackingSystem.Data.Models
{
    public class User
    {
        public string _id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; }

    }

}

