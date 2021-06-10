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
    public class RewardsController : Controller
    {
        private readonly IUserService userService;
        private readonly IProjectService projectService;
        private readonly IRewardService rewardService;
        private readonly IUserRewardService userRewardService;
        private readonly FundRaisingDbContext db;

        public RewardsController()
        {
            db = new FundRaisingDbContext();
            userService = new UserService(db);
            projectService = new ProjectService(db, userService);
            rewardService = new RewardService(db, projectService);
            userRewardService = new UserRewardService(db, userService, projectService, rewardService);
        }

        // GET: Rewards
        public IActionResult Index()
        {
            var allRewardsResult = rewardService.GetAllRewards();
            return View(allRewardsResult.Data);
        }

        // GET: Rewards/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = rewardService.GetRewardById(id.Value);

            if (reward == null)
            {
                return NotFound();
            }

            return View(reward.Data);
        }

        // GET: Rewards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rewards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reward reward)
        {
            if (ModelState.IsValid)
            {
                rewardService.CreateReward(new CreateRewardOptions
                {
                    ProjectId = reward.ProjectId,
                    Title = reward.Title,
                    Description = reward.Description,
                    Price = reward.Price

                });
            }
            return View(reward);
        }

        // GET: Rewards/Edit/5
        public IActionResult Edit(int? id, Reward rewards)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userName = HttpContext.Request.Cookies.FirstOrDefault(e => e.Key == "Username").Value;
            

            var reward = db.Rewards.Find(id);
            if (reward == null)
            {
                return NotFound();
            }
            else if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Login to edit the rewards.");
            }
            else
            {
                var project = db.Projects.FirstOrDefault(x => x.ProjectId == reward.ProjectId);
                var dbUser = db.Users.FirstOrDefault(e => e.Username == userName);
                if (dbUser == null)
                    return NotFound("User not found.");

                if (dbUser.UserId != project.CreatorId)
                    return BadRequest("Invalid user.");
            }
            return View(reward);
        }

        // POST: Rewards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reward _reward)
        {
            if (id != _reward.RewardId)
            {
                return NotFound();
            }

            var reward = rewardService.GetRewardById(id);

            if (ModelState.IsValid)
            {
                try
                {
                    rewardService.UpdateReward(id, new UpdateRewardOptions
                    {
                        Title = _reward.Title,
                        Description = _reward.Description,
                        Price = _reward.Price
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RewardExists(_reward.RewardId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(_reward);
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


        // GET: Rewards/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userName = HttpContext.Request.Cookies.FirstOrDefault(e => e.Key == "Username").Value;


            var reward = db.Rewards
                .FirstOrDefault(m => m.RewardId == id);

            if (reward == null)
            {
                return NotFound();
            }

            else if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("You must be logged in to delete the reward");
            }

            else
            {
                var dbUser = db.Users.FirstOrDefault(e => e.Username == userName);
                var dbproject = db.Projects.FirstOrDefault(x => x.ProjectId == reward.ProjectId);
                if (dbUser == null)
                    return NotFound("User not found.");

                if (dbUser.UserId != dbproject.CreatorId)
                    return BadRequest("Invalid user.");
            }

            return View(reward);
        }

        // POST: Rewards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reward = db.Rewards.Find(id);
            db.Rewards.Remove(reward);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool RewardExists(int id)
        {
            return db.Rewards.Any(e => e.RewardId == id);
        }


        // POST: Rewards/Buy/5
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        
        public IActionResult Buy(Reward reward)
        {
            userRewardService.CreateUserReward(new CreateUserRewardOptions
            {
                RewardId = reward.RewardId,
                UserId = 1
            });
            return RedirectToAction(nameof(Index));
        }


    }
}
