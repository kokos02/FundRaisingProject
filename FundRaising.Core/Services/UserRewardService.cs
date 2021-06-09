using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Options;
using FundRaising.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FundRaising.Core.Services
{
    public class UserRewardService : IUserRewardService
    {
        private readonly FundRaisingDbContext db;
        private readonly IUserService userService;
        private readonly IProjectService projectService;
        private readonly IRewardService rewardService;

        public UserRewardService(FundRaisingDbContext _db, IUserService _userService, IProjectService _projectService, IRewardService _rewardService)
        {
            db = _db;
            userService = _userService;
            projectService = _projectService;
            rewardService = _rewardService;
        }

        public Result<bool> CreateUserReward(CreateUserRewardOptions options)
        {
            if (options == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, "You are not logged in");
            }
            var userRewardIfExists = GetUserRewardById(options.UserId, options.RewardId);

            if (userRewardIfExists.Exists) //userReward = backer 
            {
                var rewardUser = userRewardIfExists.Data;
                var project = projectService.GetProjectByRewardId(options.RewardId).Data;

                if (rewardUser == null)
                {
                    return Result<bool>.ServiceFailed(StatusCode.NotFound, $"There is no backer with id: {options.UserId}");
                }

                if (!projectService.UpdateCurrentFund(project).Exists)
                {
                    return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "Reward could not be bought");
                }
                return Result<bool>.ServiceSuccessful(true);
            }
            else
            {
                var user = userService.GetUserById(options.UserId).Data;
                var reward = rewardService.GetRewardById(options.RewardId).Data;
                //var project = projectService.GetProjectByRewardId(options.RewardId).Data;

                if (user == null || reward == null )
                {
                    return Result<bool>.ServiceFailed(StatusCode.NotFound, "User, project or reward could not be found");
                }

                var userReward = new UserReward()
                {
                    User = user,
                    Reward = reward
                };
                db.UserRewards.Add(userReward);
                db.SaveChanges();

               // projectService.UpdateCurrentFund;

                
                //project.UserRewards.Add(userReward);
                //if (!projectService.UpdateCurrentFund(project).Exists)
                //{
                //    return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "Reward could not be bought");
                //}
                if (db.SaveChanges() <= 0)
                {
                    return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "UserReward could not be created");
                }
                
                return Result<bool>.ServiceSuccessful(true);

            }
        }

        public Result<UserReward> GetUserRewardById(int rewardId, int userId)
        {
            var userReward = db.Set<UserReward>()
                                .AsQueryable()
                                .Where(c => c.UserId == userId && c.RewardId == rewardId)
                                .SingleOrDefault();

            if (userReward == null)
            {
               return Result<UserReward>.ServiceFailed(StatusCode.NotFound, "Reward could not be found");
                
            }

            return Result<UserReward>.ServiceSuccessful(userReward);
        }

        public Result<List<UserReward>> GetAllUserRewards()
        {
            List<UserReward> userrewards = db.UserRewards.ToList();

            return new Result<List<UserReward>>
            {
                Data = userrewards
            };
        }

        public IQueryable<Project> GetProjectsFundedByUser(int userId)
        {
            var query = db.Set<Project>().AsQueryable().Where(c => c.UserRewards.Any(i => i.UserId == userId));
            return query;
        }
    }
}
            
                


