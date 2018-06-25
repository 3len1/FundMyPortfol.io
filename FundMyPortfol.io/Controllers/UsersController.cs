using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundMyPortfol.io.Models;
using FundMyPortfol.io.ViewModels;
using FundMyPortfol.io.Converter;

namespace FundMyPortfol.io.Controllers
{
    public class UsersController : Controller
    {
        private UsersConverter _usersConverter = new UsersConverter();
        private readonly PortofolioContext _context;

        public UsersController(PortofolioContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("userId");
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return View();
            }
            HttpContext.Response.Cookies.Append("userId", user.Id.ToString());
            return RedirectToAction("Details", user);
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.User.Include(u => u.UserDetailsNavigation);
            return View(await portofolioContext.ToListAsync());
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            UserView userView = _usersConverter.UsertoUserViewConverter(user);

            var projects = _context.Project.Where(p => p.ProjectCtrator == id);
            userView.Project = projects.ToList();

            return View(userView);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["UserDetails"] = new SelectList(_context.UserDetails, "Id", "FirstName");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,ProjectCounter,Followers,LastName,FirstName,Country,Town,Street,PostalCode,PhoneNumber,ProfileImage")] UserView userView)
        {
            User user = _usersConverter.UserViewtoUserConverter(userView);
            UserDetails userDetails = _usersConverter.UserViewtoUserDetailsConverter(userView);

            if (ModelState.IsValid)
            {
                user.UserDetailsNavigation = userDetails;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserDetails"] = new SelectList(_context.UserDetails, "Id", "FirstName", user.UserDetails);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.User
                .Include(u => u.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            UserView userView = _usersConverter.UsertoUserViewConverter(user);
            return View(userView);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Email,Password,ProjectCounter,Followers,LastName,FirstName,Country,Town,Street,PostalCode,PhoneNumber,ProfileImage")] UserView userView)
        {
            if (id != userView.Id)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            var retriveUser = _usersConverter.UserViewtoUserConverter(userView);
            user.Email = retriveUser.Email;
            user.Password = retriveUser.Password;
            UserDetails userDetails = _usersConverter.UserViewtoUserDetailsConverter(userView);
            userDetails.Id = user.UserDetails;
            userDetails.CreatedDate = user.CreatedDate;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDetails);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(userView);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var user = await _context.User.FindAsync(id);
            var userDetails = await _context.UserDetails.FindAsync(user.UserDetails);
            _context.User.Remove(user);
            _context.UserDetails.Remove(userDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        public async Task<IActionResult> LoggedEmail()
        {
            long uId;
            if (long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId))
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == uId);
                ViewData["email"] = user.Email;
            }
            return View();
        }
    }
}
