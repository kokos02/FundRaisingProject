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
    public class UserRewardsController : Controller
    {
        private readonly IUserService userService;
        private readonly IProjectService projectService;
        private readonly IRewardService rewardService;
        private readonly IUserRewardService userRewardService;
        private readonly FundRaisingDbContext db;

        public UserRewardsController()
        {
            db = new FundRaisingDbContext();
            userService = new UserService(db);
            projectService = new ProjectService(db, userService);
            rewardService = new RewardService(db, projectService);
            userRewardService = new UserRewardService(db, userService, projectService, rewardService);
        }

        // GET: UserRewards
        public IActionResult Index()
        {
            var allUserRewardsResult = userRewardService.GetAllUserRewards();
            return View(allUserRewardsResult.Data);
        }

        // GET: UserRewards/Details/5
        public IActionResult Details(int? rid, int? uid)
        {
            if (rid == null || uid == null)
            {
                return NotFound();
            }

            var userReward = userRewardService.GetUserRewardById(rid.Value, uid.Value);
            
            if (userReward == null)
            {
                return NotFound();
            }

            return View(userReward.Data);
        }

        // GET: UserRewards/Create
        public IActionResult Create()
        {
            var id = HttpContext.Request.Cookies["id"];
            var rewardid = HttpContext.Request.Cookies["RewardId"];


            userRewardService.CreateUserReward(new CreateUserRewardOptions
            {
                UserId = int.Parse(id),
                RewardId = int.Parse(rewardid)
            });

            return Content("Thank you!");
        }

        // POST: UserRewards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserReward userReward)
        {

            var id = HttpContext.Request.Cookies["id"];
            var rewardid = HttpContext.Request.Cookies["RewardId"];


                userRewardService.CreateUserReward(new CreateUserRewardOptions
                { 
                    UserId = userReward.UserId,
                    RewardId = userReward.RewardId
                });

           return View(userReward);
        }

        // GET: UserRewards/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReward = db.UserRewards.Find(id);
            if (userReward == null)
            {
                return NotFound();
            }
           return View(userReward);
        }

        // POST: UserRewards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserReward userReward)
        {
            if (id != userReward.RewardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(userReward);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRewardExists(userReward.RewardId))
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
           return View(userReward);
        }

        // GET: UserRewards/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReward = db.UserRewards
               .FirstOrDefaultAsync(m => m.RewardId == id);
            if (userReward == null)
            {
                return NotFound();
            }

            return View(userReward);
        }

        // POST: UserRewards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userReward = db.UserRewards.Find(id);
            db.UserRewards.Remove(userReward);
            db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRewardExists(int id)
        {
            return db.UserRewards.Any(e => e.RewardId == id);
        }
    }
}
