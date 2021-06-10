using FundRaising.Core;
using FundRaising.Core.Data;
using FundRaising.Core.Interfaces;
using FundRaising.Core.Models;
using FundRaising.Core.Options;
using FundRaising.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaising.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserService userService;
        private IProjectService projectService;
        private IRewardService rewardService;
        private IUserRewardService userRewardService;
        private readonly FundRaisingDbContext db;
        public UsersController()
        {
            db = new FundRaisingDbContext();
            userService = new UserService(db);
            projectService = new ProjectService(db, userService);
            rewardService = new RewardService(db, projectService);
            userRewardService = new UserRewardService(db, userService, projectService, rewardService);
        }



        // GET: Users
        public IActionResult Index()
        {
            var allUsersResult = userService.GetAllUsers();

            return View(allUsersResult.Data);
        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userService.GetUserById(id.Value);

            //if (user.ErrorCode != null || user.Data == null)
            //{
            //    return NotFound();
            //}

            return View(user.Data);
        }



        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userService.CreateUser(new CreateUserOptions
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password
                });
            }
            return View(user);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User _user)
        {
            if (id != _user.UserId)
            {
                return NotFound();
            }

            var user = userService.GetUserById(id);

            if (ModelState.IsValid)
            {
                try
                {
                    userService.GetAllUsers();

                }
                catch (DbUpdateConcurrencyException)
                {


                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userService.GetUserById(id.Value);

            if (user.ErrorText != null || user.Data == null)
            {
                return NotFound();
            }

            return View(user.Data);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Get()
        {
            var allUsersResult = userService.GetAllUsers();
            return Ok(allUsersResult.Data);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            return NoContent();
        }


    }
}


