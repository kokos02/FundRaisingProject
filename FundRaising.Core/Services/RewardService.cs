using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using Microsoft.EntityFrameworkCore;
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
              _rewardOptions.Price <= 0)
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

        public Result<List<RewardOptions>> GetAllRewards()
        {
            List<Reward> _rewards = _dbContext.Rewards.ToList();

           // List<Reward> _rewards = new();

            //_rewards.ForEach(reward => _rewards.Add(new RewardOptions()
            //{
            //    RewardId = reward.RewardId,

            //    ProjectId = reward.ProjectId,

            //    Title = reward.Title,

            //    Description = reward.Description,

            //    Price = reward.Price

            //}));

            return _rewardOptions;
        }

        public Result<RewardOptions> GetRewardById(int _rewardId)
        {
            if (_rewardId <= 0)
            {
                return new Result<RewardOptions>(ErrorCode.BadRequest, "Id cannot be less than zero.");
            }

            var reward =  _dbContext.Rewards
               .SingleOrDefault(rew => rew.RewardId == _rewardId);

            if (reward == null)
            {
                return new Result<RewardOptions>(ErrorCode.NotFound, $"Reward with id #{_rewardId} not found.");
            }            

            RewardOptions _rewardOptions = new()
            {
                RewardId = reward.RewardId,

                ProjectId = reward.ProjectId,

                Title = reward.Title,

                Description = reward.Description,

                Price = reward.Price
            };

            return new Result<RewardOptions>
            {
                Data = _rewardOptions
            };
        }

        public Result<RewardOptions> UpdateReward(int _rewardId, RewardOptions _rewardOptions)
        {
            if (_rewardOptions == null)
            {
                return new Result<RewardOptions>(ErrorCode.BadRequest, "Null options.");
            }

            if (_rewardId <= 0)
            {
                return new Result<RewardOptions>(ErrorCode.BadRequest, "Id cannot be less than zero.");
            }

            var reward = _dbContext.Rewards
               .SingleOrDefault(rew => rew.RewardId == _rewardId);

            if (reward == null)
            {
                return new Result<RewardOptions>(ErrorCode.NotFound, $"Reward with id #{_rewardId} not found.");
            }

            if (_rewardOptions.Price <= 0)
            {
                return new Result<RewardOptions>(ErrorCode.BadRequest, "Not all required user options provided correctly.");
            }

            reward.Price += _rewardOptions.Price; // Only Price????

            _dbContext.SaveChanges();

            RewardOptions rewardOptions = new()
            {
                RewardId = reward.RewardId,

                ProjectId = reward.ProjectId,

                Title = reward.Title,

                Description = reward.Description,

                Price = reward.Price
            };

            return new Result<RewardOptions>
            {
                Data = rewardOptions
            };
        }

        public Result<int> DeleteReward(int _rewardId)
        {
            var rewardToDelete = _dbContext.Rewards
                 .SingleOrDefault(rew => rew.RewardId == _rewardId);

            if (rewardToDelete == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Reward with id #{_rewardId} not found.");
            }

            _dbContext.Rewards.Remove(rewardToDelete);

            try
            {
                _dbContext.SaveChanges();
            }
            catch
            {
                return new Result<int>(ErrorCode.InternalServerError, "Could not delete Reward.");
            }

            return new Result<int>
            {
                Data = _rewardId
            };
        }
    }
}
