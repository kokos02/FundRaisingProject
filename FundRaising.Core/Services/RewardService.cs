using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            var project = projectService.GetProjectById(options.ProjectId).Data;

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

            project.AvailableRewards.Add(reward);
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

        

        public Result<bool> DeleteReward(int rewardId)
        {
            var reward = GetRewardById(rewardId).Data;
            if (reward == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, $"Reward with {rewardId} was not found");
            }

            db.Rewards.Remove(reward);
            var project = projectService.GetProjectByRewardId(rewardId).Data;
            project.AvailableRewards.Remove(reward);

            if (db.SaveChanges() <= 0)
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "The reward could not be deleted");
            }

            return Result<bool>.ServiceSuccessful(true);
        }

        public Result<bool> UpdateReward(int rewardId, UpdateRewardOptions options)
        {
            if (options == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, "Null options");
            }

            var reward = GetRewardById(rewardId).Data;

            if (reward == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, $"Reward with {rewardId} was not found");
            }

            if (reward.Title != options.Title)
            {
                reward.Title = options.Title;
            }

            if (reward.Description != options.Description)
            {
                reward.Description = options.Description;
            }

            if (reward.Price != options.Price)
            {
                reward.Price = options.Price;
            }

            if (db.SaveChanges() <= 0)
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "Reward could not be updated");
            }

            return Result<bool>.ServiceSuccessful(true);
        }


        public IQueryable<Reward> SearchRewardsByProject(SearchRewardOptions options)
        {
            var query = db.Set<Reward>().AsQueryable();

            if (options.ProjectId != null)
            {
                query = query.Where(a => a.ProjectId == options.ProjectId.Value);
            }
            return query;
        }


        public Result<List<Reward>> GetRewardsByProject(int projectId)
        {
            var rewards = SearchRewardsByProject(new SearchRewardOptions
            {
                ProjectId = projectId
            }).ToList();

            if (rewards == null)
            {
                return Result<List<Reward>>.ServiceFailed(StatusCode.NotFound, "Project could not be found");
            }
            return Result<List<Reward>>.ServiceSuccessful(rewards);
        }
    }
}
