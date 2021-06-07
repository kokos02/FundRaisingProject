using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Interfaces
{
    public interface IProjectService
    {
        public Result<Project> CreateProject(CreateProjectOptions options);
        public Result<Project> GetProjectById(int projectId);
        public Result<Project> GetProjectByRewardId(int rewardId);
        public Result<bool> UpdateCurrentFund(Project project);
        
        public Result<List<Project>> GetAllProjects();
        //public Result<ProjectOptions> UpdateProject(int _projectId, ProjectOptions _projectOptions);
        public Result<bool> DeleteProject(int projectId);


    }
}
