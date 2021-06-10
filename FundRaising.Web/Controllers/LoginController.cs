using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundRaising.Core.Data;
using FundRaising.Core.Models;


namespace FundRaising.Web.Controllers
{
    public class LoginController : Controller
    {
        //private readonly FundRaisingDbContext _context;

        //public LoginController(FundRaisingDbContext context)
        //{
        //    _context = context;
        //}

        private readonly FundRaisingDbContext db;

        public LoginController()
        {
            db = new FundRaisingDbContext();
        }

        // GET: Login
        public IActionResult Index()
        {
            return View(new User());
        }

        // GET: Login/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = db.Users
                .FirstOrDefault(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Add(user);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Login/Edit/5
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

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (user.Username == null || user.Password == null)
                return BadRequest("Please provide valid username and password.");

            var dbUser = db.Users.FirstOrDefault(e => e.Username == user.Username);

            if (dbUser == null)
                return NotFound("Username does not exist.");
            else if (dbUser.Password != user.Password)
                return BadRequest("The password is invalid.");

            HttpContext.Response.Cookies.Append("Username", user.Username);
            HttpContext.Response.Cookies.Append("id", dbUser.UserId.ToString());
            //HttpContext.Response.Cookies.Append()


            return RedirectToAction("Index", "Home");
        }

        // GET: Login/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = db.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return db.Users.Any(e => e.UserId == id);
        }
    }
}
