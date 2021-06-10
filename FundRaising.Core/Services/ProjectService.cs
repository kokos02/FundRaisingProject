using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if(options.ProjectCategory == null)
            {
                return Result<Project>.ServiceFailed(StatusCode.BadRequest, "You have to choose a category");
            }
            
            var category = (ProjectCategory)Enum.Parse(typeof(ProjectCategory), options.ProjectCategory, true);

            var project = new Project
            {
                CreatorId = options.CreatorId,
                Title = options.Title,
                Description = options.Description,
                ProjectCategory = category,
                Deadline = options.Deadline,
                TargetFund = options.TargetFund
            };

            user.Projects.Add(project);
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
            var project = SearchProject(new SearchProjectOptions
            {
                RewardId = rewardId
            }).Include(a => a.UserRewards).ThenInclude(a => a.Reward).SingleOrDefault();

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

        public Result<bool> UpdateProject(int projectId, UpdateProjectOptions options)
        {
            if (options == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.BadRequest, "Null options.");
            }

            var project = GetProjectById(projectId).Data;

            if (project == null)
            {
                return Result<bool>.ServiceFailed(StatusCode.NotFound, $"Project with ID {projectId} was not found");
            }

            if (project.Title != options.Title)
            {
                project.Title = options.Title;
            }

            if (project.Description != options.Description)
            {
                project.Description = options.Description;
            }

            var category = (ProjectCategory)Enum.Parse(typeof(ProjectCategory), options.ProjectCategory, true);
            if (project.ProjectCategory != category)
            {
                project.ProjectCategory = category;
            }

            if (project.TargetFund != options.TargetFund)
            {
                project.TargetFund = options.TargetFund;
            }

            if (project.Deadline != options.Deadline)
            {
                project.Deadline = options.Deadline;
            }

            if (db.SaveChanges() <= 0)
            {
                return Result<bool>.ServiceFailed(StatusCode.InternalServerError, "Project could not be updated");
            }
            return Result<bool>.ServiceSuccessful(true);

        }








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

        public IQueryable<Project> SearchProject(SearchProjectOptions options)
        {
            var query = db.Set<Project>().AsQueryable();

            if(options.RewardId != null)
            {
                query = query.Where(c => c.AvailableRewards.Any(a => a.RewardId == options.RewardId.Value)).Include(a => a.AvailableRewards);
            }

            return query;
        }
    }
}

            




            




            

            

            



            





       

