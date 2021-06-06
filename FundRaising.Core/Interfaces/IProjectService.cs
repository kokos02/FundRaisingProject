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
        public Result<Project> CreateProject(ProjectOptions _projectOptions);
        public Result<List<ProjectOptions>> GetAllProjects();
        public Result<ProjectdOptions> GetProjectdById(int _projectId);
        public Result<ProjectOptions> UpdateProject(int _projectId, ProjectOptions _projectOptions);
        public Result<int> DeleteProject(int _projectId);
    }
}
