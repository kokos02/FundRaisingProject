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
        private readonly FundRaisingDbContext db;
        private readonly IUserService userService;
        public ProjectService(FundRaisingDbContext _db, IUserService _userService)
        {
            db = _db;
            userService = _userService;
        }

        public Result<Project> CreateProject(CreateProjectOptions options)
        {
            if (options == null)
            {
                return Result<Project>.ServiceFailed(StatusCode.BadRequest, "Null options");
            }

            var user = userService.GetUserById(options.CreatorId).Data;
            if (user == null)
            {
                return Result<Project>.ServiceFailed(StatusCode.NotFound, $"User with Id: {options.CreatorId} was not found");
            }

            var categ = (ProjectCategory)Enum.Parse(typeof(ProjectCategory), options.Category, true);

            var project = new Project
            {
                CreatorId = options.CreatorId,
                Title = options.Title,
                Description = options.Description,
                ProjectCategory = categ,
                Deadline = options.Deadline,
                TargetFund = options.TargetFund
            };

            db.Projects.Add(project);
            if (db.SaveChanges() <= 0)
            {
                return Result<Project>.ServiceFailed(StatusCode.InternalServerError, "Project could not be created");
            }

            return Result<Project>.ServiceSuccessful(project);
        }

        public Result<Project> GetProjectById(int projectId)
        {
            var project = db.Projects.Find(projectId);
            if (project == null)
            {
                return Result<Project>.ServiceFailed(StatusCode.NotFound, $"There is no user with this Id: {projectId}");
            }
            return Result<Project>.ServiceSuccessful(project);
        }

        public Result<Project> GetProjectByRewardId(int rewardId)
        {
            
            var project = db.Projects.Find(rewardId);
            if (project == null)
            {
                return Result<Project>.ServiceFailed(StatusCode.NotFound, "Project could not be found");
            }
            return Result<Project>.ServiceSuccessful(project);
        }

        public Result<bool> UpdateCurrentFund(Project project)
        {
            if (project == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.NotFound, "Project could not be found");
            }

            decimal sum = 0m;

            foreach (var x in project.UserRewards)
            {
                sum += x.Reward.Price;
            }

            project.CurrentFund = sum;

            if (db.SaveChanges() <= 0)
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "Current Fund could not be updated");
            }
            return Result<bool>.ServiceSuccessful(true);
        }

        public Result<List<Project>> GetAllProjects()
        {
            List<Project> projects = db.Projects.ToList();

            return new Result<List<Project>>
            {
                Data = projects
            };
        }




            




        //public Result<Project> UpdateProject(int _projectId, ProjectOptions _projectOptions)
        //{
        //    if (_projectOptions == null)
        //    {
        //        return Result<Project>.ServiceFailed(StatusCode.BadRequest, "Null options.");
        //    }

        //    if (_projectId <= 0)
        //    {
        //        return  Result<Project>.ServiceFailed(StatusCode.BadRequest, "Id cannot be less than zero.");
        //    }

        //    var project = db.Projects
        //       .SingleOrDefault(rew => rew.ProjectId == _projectId);

        //    if (project == null)
        //    {
        //        return Result<Project>.ServiceFailed(StatusCode.NotFound, $"Project with id #{_projectId} not found.");
        //    }

        //    if (_projectOptions.CurrentFund <= 0)
        //    {
        //        return Result<Project>.ServiceFailed(StatusCode.BadRequest, "Not all required project options provided correctly.");
        //    }

            

        //    ProjectOptions projectOptions = new()
        //    {
        //        ProjectId = project.ProjectId,

        //        CreatorId = project.CreatorId,

        //        Title = project.Title,

        //        Description = project.Description,

        //        //ProjectCategory = project.ProjectCategory,

        //        Deadline = project.Deadline,

        //        CurrentFund = project.CurrentFund,

        //        TargetFund = project.TargetFund

        //    };

            
        //}

        public Result<bool> DeleteProject(int projectId)
        {
           var projectToDelete = db.Projects.SingleOrDefault(project => project.ProjectId == projectId);

            if (projectToDelete == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.NotFound, $"Project with id #{projectId} not found");
            }

            db.Projects.Remove(projectToDelete);

            try
            {
                db.SaveChanges();
            }
            catch
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "Could not delete Project");
            }

            return Result<bool>.ServiceSuccessful(true);
        }







    }
}

