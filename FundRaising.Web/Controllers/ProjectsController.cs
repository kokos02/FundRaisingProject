using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundRaising.Core.Data;
using FundRaising.Core.Models;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Services;
using FundRaising.Core.Options;

namespace FundRaising.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private IUserService userService;
        private IProjectService projectService;
        private IRewardService rewardService;
        private IUserRewardService userRewardService;
        private readonly FundRaisingDbContext db;
        public ProjectsController()
        {
            db = new FundRaisingDbContext();
            userService = new UserService(db);
            projectService = new ProjectService(db, userService);
            rewardService = new RewardService(db, projectService);
            userRewardService = new UserRewardService(db, userService, projectService, rewardService);
        }

        /*private IUserRewardService _userRewardService;
        public ProjectsController(IUserRewardService userRewardService)
        {
            _userRewardService = userRewardService;
        }*/


        // GET: Projects
        public IActionResult Index()
        {
            var allProjectsResult = projectService.GetAllProjects();
            return View(allProjectsResult.Data);
        }

        // GET: Projects/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = projectService.GetProjectById(id.Value);
                
            if (project == null)
            {
                return NotFound();
            }

            return View(project.Data);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ShowProjectRewards(int id)
        {
            var rewards = rewardService.GetRewardsByProject(id).Data;
            return View(rewards);
        }

        public IActionResult Product(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = db.Projects.Find(id);
        
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                projectService.CreateProject(new CreateProjectOptions
                {
                    CreatorId = project.CreatorId,
                    Title = project.Title,
                    Description = project.Description,
                    ProjectCategory = project.ProjectCategory.ToString(),
                    Deadline = project.Deadline,
                    TargetFund = project.TargetFund,

                }); 
            }
            return View(project);
        }
                
                

        // GET: Projects/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userName = HttpContext.Request.Cookies.FirstOrDefault(e => e.Key == "Username").Value;

            var project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            else if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Login to edit the projects.");
            }
            else
            {
                var dbUser = db.Users.FirstOrDefault(e => e.Username == userName);
                if (dbUser == null)
                    return NotFound("User not found.");

                if (dbUser.UserId != project.CreatorId)
                    return BadRequest("Invalid user.");
            }

            return View(project);
        }

        // POST: Home/Login/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (user.Username == null || user.Password == null)
                return BadRequest("Please provide a valid username and password combination.");

            var dbUser = db.Users
                .FirstOrDefault(m => m.Username == user.Username);

            if (dbUser == null)
                return NotFound("User not found.");
            else if (dbUser.Password != user.Password)
                return BadRequest("The password is incorrect.");

            HttpContext.Response.Cookies.Append("Username", user.Username);
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = db.Projects
                .FirstOrDefault(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Any(e => e.ProjectId == id);
        }
    }
}
