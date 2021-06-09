using System;
using System.Collections.Generic;

namespace FundRaising.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public List<Project> Projects { get; set; }

        public User()
        {
            Projects = new List<Project>();
            Created = DateTime.Now;
        }

    }
}
