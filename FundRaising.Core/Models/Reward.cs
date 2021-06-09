using System;
using System.Collections.Generic;

namespace FundRaising.Core.Models
{
    public class Reward
    {
        public int RewardId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public List<UserReward> UserReward { get; set; }

        public Reward()
        {
            Created = DateTime.Now;
        }
    }
}
