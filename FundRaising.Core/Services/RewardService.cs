using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Services
{
    public class RewardService : IRewardService
    {
        private readonly FundRaisingDbContext _dbContext;
        public RewardService(FundRaisingDbContext _db)
        {
            _dbContext = _db;
        }
        public RewardOptions CreateReward(RewardOptions _rewardOptions)
        {
            Reward _newReward = new()
            {
                Title = _rewardOptions.Title,

                Description = _rewardOptions.Description,

                Price = _rewardOptions.Price

            };

            _dbContext.Rewards.Add(_newReward);

            _dbContext.SaveChanges();


            return new RewardOptions
            {
                RewardId = _newReward.RewardId,

                Project = _newReward.Project,

                Title = _newReward.Title,

                Description = _newReward.Description,

                Price = _newReward.Price
            };
        }

        
        public List<RewardOptions> GetAllRewards()
        {
            List<Reward> _rewards = _dbContext.Rewards.ToList();

            List<RewardOptions> _rewardOptions = new();

            _rewards.ForEach(reward => _rewardOptions.Add(new RewardOptions()
            {
                RewardId = reward.RewardId,

                Project = reward.Project,

                Title = reward.Title,

                Description = reward.Description,

                Price = reward.Price

            }));

            return _rewardOptions;
        }

        public RewardOptions GetRewardById(int _rewardId)
        {
            Reward _reward = _dbContext.Rewards.Find(_rewardId);

            RewardOptions _rewardOptions = new()
            {
                RewardId = _reward.RewardId,

                Project = _reward.Project,

                Title = _reward.Title,

                Description = _reward.Description,

                Price = _reward.Price
            };

            return _rewardOptions;
        }

        public bool UpdateReward(int _rewardId, RewardOptions _rewardOptions)
        {
            if (_rewardOptions == null) return false;

            Reward _reward = _dbContext.Rewards.FirstOrDefault(reward => reward.RewardId == _rewardId);

            _reward.Title = _rewardOptions.Title;

            _reward.Description = _rewardOptions.Description;

            _reward.Price = _rewardOptions.Price;

            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteReward(int _rewardId)
        {
            Reward _reward = _dbContext.Rewards.Find(_rewardId);

            if (_reward == null) return false;

            _dbContext.Rewards.Remove(_reward);

            return true;
        }
    }
}
