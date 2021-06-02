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
        public ProjectOptions CreateProject(ProjectOptions _projectOptions, UserOptions _userOptions);
        public List<ProjectOptions> GetAllProjects();
        public ProjectOptions GetProjectById(int _projectId);
        public bool UpdateProject(int _projectId, ProjectOptions _projectOptions);
        public bool DeleteProject(int _projectId);
    }
}
