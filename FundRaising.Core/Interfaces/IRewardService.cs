﻿using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Interfaces
{
    interface IRewardService
    {
        public RewardOptions CreateReward(RewardOptions _rewardOptions);
        public List<RewardOptions> GetAllRewards();
        public RewardOptions GetRewardById(int _rewardId);
        public bool UpdateReward(int _rewardId, RewardOptions _rewardOptions);
        public bool DeleteReward(int _rewardId);
    }
}
