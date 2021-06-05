using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Options;
using FundRaising.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Services
{
    public class RewardUserService : IRewardUserService
    {
        private readonly IFundRaisingDbContext _dbContext;
       // private readonly IProjectService _projectService;
        //private readonly IUserService _userService;

        public RewardUserService(IFundRaisingDbContext _db)//, IProjectService _projectServ, IUserService _userServ)
        {
            _dbContext = _db;
           // _projectService = _projectServ;
          //  _userService = _userServ;
        }
        public bool PurchaseReward(int rewardId)
        {
            var reward = _dbContext.Rewards.Find(rewardId);
            var project = _dbContext.Projects.Find(reward.ProjectId);
            project.CurrentFund += reward.Price;
            
            _dbContext.SaveChanges();
            return true;

            //var user = _userService.GetUserById(_userOptions.UserId);
            
            //var project = _projectService.GetProjectById(_projectOptions.ProjectId);
            //project.CurrentFund += _rewardOptions.Price;
            //return true;
            //User _user = _dbContext.Users.Find(_userOptions.UserId);

            //Project _project = _dbContext.Projects.Find(_projectOptions.ProjectId);

            //_project.CurrentFund += _amount;

            //Fund _fund = new()
            //{
            //    User = _user,
            //    Project = _project
            //};
           


            //_dbContext.Funds.Add(_fund);
            //_dbContext.SaveChanges();

            //return true;

            
        }
    }
}
            
                


