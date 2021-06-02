﻿using FundRaising.Core.Data;
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
        private readonly FundRaisingDbContext _dbContext;
        public ProjectService(FundRaisingDbContext _db)
        {
            _dbContext = _db;
        }
        
            
        public ProjectOptions CreateProject(ProjectOptions _projectOptions, UserOptions _userOptions)
        {
            
             Project _newProject = new()
             {
                Title = _projectOptions.Title,

                Description = _projectOptions.Description,

                //ProjectCategory = _projectOptions.ProjectCategory,

                Deadline = _projectOptions.Deadline,

                TargetFund = _projectOptions.TargetFund,

                CreatorId = _userOptions.UserId
             };

             _dbContext.Projects.Add(_newProject);

             _dbContext.SaveChanges();


            return new ProjectOptions
            {
                ProjectId = _newProject.ProjectId,

                CreatorId = _newProject.CreatorId, //Creators id

                Title = _newProject.Title,

                Description = _newProject.Description,

                TargetFund = _newProject.TargetFund
            };
        }


        public List<ProjectOptions> GetAllProjects()
        {
            List<Project> _projects = _dbContext.Projects.ToList();

            List<ProjectOptions> _projectOptions = new();

            _projects.ForEach(project => _projectOptions.Add(new ProjectOptions()
            {
                ProjectId = project.ProjectId,

                CreatorId = project.CreatorId,

                Title = project.Title,

                Description = project.Description,

                //Category = project.ProjectCategory,

                Deadline = project.Deadline,

                TargetFund = project.TargetFund

            }));


            return _projectOptions;

        }

        public ProjectOptions GetProjectById(int _projectId)
        {
            Project _project = _dbContext.Projects.Find(_projectId);

            ProjectOptions _projectOptions = new()
            {
                ProjectId = _project.ProjectId,

                CreatorId = _project.CreatorId,

                Title = _project.Title,

                Description = _project.Description,

                //ProjectCategory = _project.ProjectCategory,

                Deadline = _project.Deadline,

                TargetFund = _project.TargetFund

            };

            return _projectOptions;
        }

        public bool UpdateProject(int _projectId, ProjectOptions _projectOptions)
        {
            if (_projectOptions == null) return false;

            Project _project = _dbContext.Projects.FirstOrDefault(project => project.ProjectId == _projectId);

            _project.Title = _projectOptions.Title;

            _project.Description = _projectOptions.Description;

            //_project.ProjectCategory = _projectOptions.ProjectCategory;

            _project.Deadline = _projectOptions.Deadline;

            _project.TargetFund = _projectOptions.TargetFund;

            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteProject(int _projectId)
        {
            Project _project = _dbContext.Projects.Find(_projectId);

            if (_project == null) return false;

            _dbContext.Projects.Remove(_project);

            return true;
        }
    }
}
