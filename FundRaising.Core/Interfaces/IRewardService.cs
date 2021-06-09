using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System.Collections.Generic;
using System.Linq;

namespace FundRaising.Core.Interfaces
{
    public interface IRewardService
    {
        public Result<Reward> CreateReward(CreateRewardOptions options);
        public Result<Reward> GetRewardById(int rewardId);
        public Result<List<Reward>> GetAllRewards();
        public Result<bool> UpdateReward(int rewardId, UpdateRewardOptions options);
        public Result<bool> DeleteReward(int rewardId);
        public IQueryable<Reward> SearchRewardsByProject(SearchRewardOptions options);
        public Result<List<Reward>> GetRewardsByProject(int projectId);
    }
}

