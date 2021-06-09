using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System.Collections.Generic;
using System.Linq;

namespace FundRaising.Core.Interfaces
{
    public interface IUserRewardService
    {
        public Result<bool> CreateUserReward(CreateUserRewardOptions options);
        public Result<UserReward> GetUserRewardById(int rewardId, int userId);
        public Result<List<UserReward>> GetAllUserRewards();
        public IQueryable<Project> SearchProjectsFundedByUser(SearchProjectsFunded options);
        public Result<List<Project>> GetProjectsFundedByUser(int userId);
    }
}


