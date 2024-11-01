using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Unauthorized()
        {
            return View();
        }
        // GET: User
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name.Split('\\')[1].ToString();
            Users user = _context.Users.Where(x => x.FirstName == userName).FirstOrDefault();
            if (user == null || (user.IsSuperAdmin ?? false)==false)
            {
               return RedirectToAction("Unauthorized");
            }
            else
            {
                ViewBag.IsAdmin = user.IsAdmin??false;
                ViewBag.IsSuperAdmin = user.IsSuperAdmin??false;
            };
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return View();
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            Users user = new Users();
            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                user.IsAdmin = user.IsAdmin ?? false;
                user.IsSuperAdmin = user.IsSuperAdmin ?? false;
                user.CreatedAt = DateTime.Now;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return View();
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Users user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = id;
                user.IsAdmin = user.IsAdmin ?? false;
                user.IsSuperAdmin = user.IsSuperAdmin ?? false;
                var existedUser = _context.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
                user.CreatedAt = existedUser.CreatedAt;
                _context.Users.AddOrUpdate(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}