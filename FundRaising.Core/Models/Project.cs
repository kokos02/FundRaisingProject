using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectCategory ProjectCategory { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public decimal CurrentFund { get; set; }
        public decimal TargetFund { get; set; }
        public List<Reward> AvailableRewards { get; set; }
        public List<UserReward> UserRewards { get; set; }
        public Project()
        {
            CurrentFund = 0m;
            Created = DateTime.Now;
            AvailableRewards = new List<Reward>();
            UserRewards = new List<UserReward>();
        }
    }
}
        
        


