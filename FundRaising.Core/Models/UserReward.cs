using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Models
{
    public class UserReward
    {
        public int UserId { get; set; }
        public int RewardId { get; set; }
        public User User { get; set; }
        public Reward Reward { get; set; }
    }
}
        

        
