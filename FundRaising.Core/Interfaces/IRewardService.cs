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
        public Result<Reward> CreateReward(RewardOptions _rewardOptions);
        public Result<List<RewardOptions>> GetAllRewards();
        public Result<RewardOptions> GetRewardById(int _rewardId);
        public Result<RewardOptions> UpdateReward(int _rewardId, RewardOptions _rewardOptions);
        public Result<int> DeleteReward(int _rewardId);
    }
}
