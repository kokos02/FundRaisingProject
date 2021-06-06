using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IFundRaisingDbContext _dbContext;
        //private readonly ILogger<ProjectService> _projectService;
        private readonly IProjectService _projectService;

        public ProjectService(IFundRaisingDbContext _db)//, UserService _us)
        {
            _dbContext = _db;
            _projectService = projectservice;
        }
        
        public Result<Project> CreateProject(ProjectOptions _projectOptions)    
        {
            //validation
            if (_projectOptions == null)
            {
                return new Result<Project>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(_projectOptions.Title) ||
                string.IsNullOrWhiteSpace(_projectOptions.Description) ||
                string.IsNullOrWhiteSpace(_projectOptions.Category) ||
                //Deadline validation
                _projectOptions.TargetFund < = 0)
            {
                return new Result<Project>(ErrorCode.BadRequest, "Not all required project options provided")
            }

            var projectWithSameTitle = _dbContext.Projects.SingleOrDefault(project => project.Title == _projectOptions.Title);

            if (projectWithSameTitle != null)
            {
                return new Result<Project>(ErrorCode.Conflict, $"Project named #{_projectOptions.Title} already exists.");
            }


             };

             _dbContext.Projects.Add(_newProject);

             _dbContext.SaveChanges();


            return new ProjectOptions
            {
                ProjectId = _newProject.ProjectId,

                CreatorId = _newProject.CreatorId,

                Title = _newProject.Title,

                Description = _newProject.Description,
                
                TargetFund = _newProject.TargetFund
            };
        }


        public Result<List<ProjectOptions>> GetAllProjects()
        {
            List<Project> _projects = _dbContext.Projects.ToList();

            List<ProjectOptions> _projectOptions = new();

            _projects.ForEach(project => _projectOptions.Add(new ProjectOptions()
            {
                ProjectdId = project.ProjectdId,

                CreatorId = project.CreatorId,

                Title = project.Title,

                Description = project.Description,

                Catregory = project.Category,

                Deadline = project.Deadline,

                CurrentFund = project.CurrentFund,

                TargetFund = project.TargetFund

            }));

            return new Result<List<ProjectOptions>>
            {
                Data = _projectOptions
            };
        }

           public Result<ProjectdOptions> GetProjectdById(int _projectId)
        {
            if (_projectId <= 0)
            {
                return new Result<ProjectOptions>(ErrorCode.BadRequest, "Id cannot be less than zero.");
            }

            var project =  _dbContext.Projects
               .SingleOrDefault(rew => rew.ProjectId == _projectId);

            if (project == null)
            {
                return new Result<ProjectOptions>(ErrorCode.NotFound, $"Project with id #{_projectId} not found.");
            }            

            ProjectOptions _projectOptions = new()
            {
                 ProjectdId = project.ProjectdId,

                CreatorId = project.CreatorId,

                Title = project.Title,

                Description = project.Description,

                Catregory = project.Category,

                Deadline = project.Deadline,

                CurrentFund = project.CurrentFund,

                TargetFund = project.TargetFund
            };

            return new Result<ProjectOptions>
            {
                Data = _projectoptions
            };
        }

        
        public Result<ProjectOptions> UpdateProject(int _projectId, ProjectOptions _projectOptions)
        {
            if (_projectOptions == null)
            {
                return new Result<ProjectOptions>(ErrorCode.BadRequest, "Null options.");
            }

            if (_projectId <= 0)
            {
                return new Result<ProjectOptions>(ErrorCode.BadRequest, "Id cannot be less than zero.");
            }

            var project = _dbContext.Projects
               .SingleOrDefault(rew => rew.ProjectId == _ProjectId);

            if (project == null)
            {
                return new Result<ProjectOptions>(ErrorCode.NotFound, $"Project with id #{_projectId} not found.");
            }

            if (_projectOptions.CurrentFund <= 0)
            {
                return new Result<ProjeOptions>(ErrorCode.BadRequest, "Not all required project options provided correctly.");
            }

            project.CurrentFund += _projectOptions.CurrentFund; 

            _dbContext.SaveChanges();

            ProjectOptions projectOptions = new()
            {
                ProjectdId = project.ProjectdId,

                CreatorId = project.CreatorId,

                Title = project.Title,

                Description = project.Description,

                Catregory = project.Category,

                Deadline = project.Deadline,

                CurrentFund = project.CurrentFund,

                TargetFund = project.TargetFund

            };

            return new Result<ProjectOptions>
            {
                Data = projectOptions
            };
        }

        public Result<int> DeleteProject(int _projectId)
        {
           var projectToDelete = _dbContext.Projects.SingleOrDefault(project.ProjectId == _projectId);

            if (projectToDelete == null)
            {
                return new Result<int>(ErrorCOde.NotFound, $"Project with id #{_projectId} not found");
            }

            _dbContext.Projects.Remove(projectToDelete);

            try
            {
                _dbContext.SaveChanges();

            }

             catch
            {
                return new Result<int>(ErrorCode.InternalServerError, "Could not delete Project");
            }
            
            return new Result<int>
            {
                Data = _projectId
            };
        }
    }
}

