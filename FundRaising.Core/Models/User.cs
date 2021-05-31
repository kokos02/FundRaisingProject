using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public List<Project> Projects { get; set; }
        public User()
        {
            Created = DateTime.Now;
            Projects = new List<Project>();
        }
    }
}
