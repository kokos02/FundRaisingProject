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
        private readonly FundRaisingDbContext db;
        private readonly IProjectService projectService;
        public RewardService(FundRaisingDbContext _db, IProjectService _projectService)
        {
            db = _db;
            projectService = _projectService;
        }
        public Result<Reward> CreateReward(CreateRewardOptions options)
        {
            if (options == null)
            {
                return Result<Reward>.ServiceFailed(StatusCode.BadRequest, "Null options");
            }
            var project = projectService.GetProjectById(options.ProjectId);

            if (project == null)
            {
                return Result<Reward>.ServiceFailed(StatusCode.NotFound, $"Project with id: {options.ProjectId} was not found");
            }

            var reward = new Reward
            {
                ProjectId = options.ProjectId,
                Title = options.Title,
                Description = options.Description,
                Price = options.Price
            };

            db.Rewards.Add(reward);
            if (db.SaveChanges() <= 0)
            {
                return Result<Reward>.ServiceFailed(StatusCode.InternalServerError, "Reward could not be created");
            }
            return Result<Reward>.ServiceSuccessful(reward);
        }

        public Result<List<Reward>> GetAllRewards()
        {
            List<Reward> rewards = db.Rewards.ToList();

            return new Result<List<Reward>>
            {
                Data = rewards
            };
        }

        public Result<Reward> GetRewardById(int rewardId)
        {
            var reward = db.Rewards.Find(rewardId);
            if (reward == null)
            {
                return Result<Reward>.ServiceFailed(StatusCode.NotFound, "Reward could not be found");
            }
            return Result<Reward>.ServiceSuccessful(reward);
        }

        //public Result<Reward> UpdateReward(int _rewardId, RewardOptions _rewardOptions)
        //{
        //    if (_rewardOptions == null)
        //    {
        //        return Result<Reward>.ServiceFailed(StatusCode.BadRequest, "Null options.");
        //    }
        //    if (_rewardId <= 0)
        //    {
        //        return Result<Reward>.ServiceFailed(StatusCode.BadRequest, "Id cannot be less than zero.");
        //    }
        //    var reward = db.Rewards.SingleOrDefault(rew => rew.RewardId == _rewardId);
        //    if (reward == null)
        //    {
        //        return Result<Reward>.ServiceFailed(StatusCode.NotFound, $"Reward with id #{_rewardId} not found.");
        //    }
        //    if (_rewardOptions.Price <= 0)
        //    {
        //        return Result<Reward>.ServiceFailed(StatusCode.BadRequest, "Not all required user options provided correctly.");
        //    }
        //    _dbContext.SaveChanges();
        //    RewardOptions rewardOptions = new()
        //    {
        //        RewardId = reward.RewardId,
        //        ProjectId = reward.ProjectId,
        //        Title = reward.Title,
        //        Description = reward.Description,
        //        Price = reward.Price
        //    };
        //    return new Result<RewardOptions>
        //    {
        //        Data = rewardOptions
        //    };
        //}

        public Result<bool> DeleteReward(int rewardId)
        {
            var reward = GetRewardById(rewardId).Data;
            if (reward == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, $"Reward with {rewardId} was not found");
            }

            db.Rewards.Remove(reward);
            if (db.SaveChanges() <= 0)
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "The reward could not be deleted");
            }

            return Result<bool>.ServiceSuccessful(true);
        }













    }
}
