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
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public List<Project> CreatedProjects { get; set; }
        public List<Project> FundedProjects { get; set; }
        public List<UserReward> PurchasedRewards { get; set; }
        public User()
        {
            Created = DateTime.Now;
            CreatedProjects = new List<Project>();
            FundedProjects = new List<Project>();
            PurchasedRewards = new List<UserReward>();
        }
    }
}
