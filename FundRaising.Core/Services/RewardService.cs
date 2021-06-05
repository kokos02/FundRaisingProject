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
        private readonly IFundRaisingDbContext _dbContext;
        private readonly IProjectService _projectService;
        
        public RewardService(IFundRaisingDbContext _db, IProjectService projectservice)
        {
            _dbContext = _db;
            _projectService = projectservice;
        }
        public Result<Reward> CreateReward(RewardOptions _rewardOptions)
        {
            if (_rewardOptions == null)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(_rewardOptions.Title) ||
              string.IsNullOrWhiteSpace(_rewardOptions.Description) ||
              _rewardOptions.Price <=0)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Not all required user options provided correctly.");
            }

            var _rewardWithSameTitle = _dbContext.Rewards.SingleOrDefault(reward => reward.Title == _rewardOptions.Title);

            if (_rewardWithSameTitle != null)
            {
                return new Result<Reward>(ErrorCode.Conflict, $"Reward with #{_rewardOptions.Title} already exists.");
            }

            Reward _newReward = new()
            {
                ProjectId = _rewardOptions.ProjectId,
                Title = _rewardOptions.Title,

                Description = _rewardOptions.Description,

                Price = _rewardOptions.Price

            };

            _dbContext.Rewards.Add(_newReward);
            try
            {
                _dbContext.SaveChanges();
            }
            catch 
            {
                return new Result<Reward>(ErrorCode.InternalServerError, "Could not save user.");
            }

            return new Result<Reward>
            {
                Data = _newReward
            };

        }


            

        
        public Result<List<Reward>> GetAllRewards()
        {
            List<Reward> _rewards = _dbContext.Rewards.ToList();

            List<RewardOptions> _rewardOptions = new();

            _rewards.ForEach(reward => _rewardOptions.Add(new RewardOptions()
            {
                RewardId = reward.RewardId,

                ProjectId = reward.ProjectId,

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

                ProjectId = _reward.ProjectId,

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
