using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Interfaces
{
    public interface IRewardService
    {
        public Result<Reward> CreateReward(CreateRewardOptions options);
        public Result<Reward> GetRewardById(int rewardId);
        public Result<List<Reward>> GetAllRewards();
        //public Result<Reward> UpdateReward(int rewardId, RewardOptions rewardOptions);
        public Result<bool> DeleteReward(int rewardId);
    }
}
