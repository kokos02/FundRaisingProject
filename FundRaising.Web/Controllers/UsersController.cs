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
using FundRaising.Core;
using FundRaising.Core.Options;

namespace FundRaising.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFundRaisingDbContext _db;
        public UsersController(IFundRaisingDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }



        // GET: Users
        public IActionResult Index()
        {
            var allUsersResult = _userService.GetAllUsers();

            return View(allUsersResult.Data);
        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUserById(id.Value);

            if (user.Error != null || user.Data == null)
            {
                return NotFound();
            }

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
                _userService.CreateUser(new UserOptions
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password
                });
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

            var user = _userService.GetUserById(id);

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.GetAllUsers();

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

            var user = _userService.GetUserById(id.Value);

            if (user.Error != null || user.Data == null)
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
            _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Get()
        {
            var allUsersResult = _userService.GetAllUsers();
            return Ok(allUsersResult.Data);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }


    }
}


