using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Interfaces
{
    public interface IUserRewardService
    {
        public Result<bool> CreateUserReward(CreateUserRewardOptions options);
        public Result<UserReward> GetUserRewardById(int rewardId, int userId);
        public Result<List<UserReward>> GetAllUserRewards();
    }
}
