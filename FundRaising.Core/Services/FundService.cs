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
    public class FundService : IFundService
    {
        private readonly IFundRaisingDbContext _dbContext;
        public FundService(IFundRaisingDbContext _db)
        {
            _dbContext = _db;
        }
        public bool FundProject(ProjectOptions _projectOptions, decimal _amount,UserOptions _userOptions)
        {
            User _user = _dbContext.Users.Find(_userOptions.UserId);

            Project _project = _dbContext.Projects.Find(_projectOptions.ProjectId);

            _project.CurrentFund += _amount;

            Fund _fund = new()
            {
                User = _user,
                Project = _project
            };
           


            _dbContext.Funds.Add(_fund);
            _dbContext.SaveChanges();

            return true;

            
        }
    }
}
            
                


