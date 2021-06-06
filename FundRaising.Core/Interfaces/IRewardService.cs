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
        public bool GetAllRewards();
        public RewardOptions GetRewardById(int _rewardId);
        public bool UpdateReward(int _rewardId, RewardOptions _rewardOptions);
        public bool DeleteReward(int _rewardId);
    }
}
