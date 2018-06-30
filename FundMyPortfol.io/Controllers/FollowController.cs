using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundMyPortfol.io.Models;
using Microsoft.AspNetCore.Authorization;

namespace FundMyPortfol.io.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly PortofolioContext _context;

        public FollowController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: Follow
        public async Task<IActionResult> Index()
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if (uId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            var portofolioContext = _context.BackerFollowCreator.Include(u => u.BackerNavigation).Include(u => u.ProjectCreatorNavigation)
                .Include(d => d.BackerNavigation.UserDetailsNavigation).Include(d => d.ProjectCreatorNavigation.UserDetailsNavigation)
                .Where(b => b.Backer == uId);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Follow/Followers
        public async Task<IActionResult> Followers()
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if (uId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            var portofolioContext = _context.BackerFollowCreator.Include(u => u.BackerNavigation).Include(u => u.ProjectCreatorNavigation)
                .Include(d => d.BackerNavigation.UserDetailsNavigation).Include(d => d.ProjectCreatorNavigation.UserDetailsNavigation)
                .Where(b => b.ProjectCreator == uId);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Follow/FollowForm
        public IActionResult FollowForm(long? Id)
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if (uId == 0 || Id == null)
                return RedirectToAction("Login", "Users");
            else if (uId == Id)
                return Forbid();
            var backer = _context.User.Include(u => u.UserDetailsNavigation).FirstOrDefault(u => u.Id == uId);
            var creator = _context.User.Include(u => u.UserDetailsNavigation).FirstOrDefault(u => u.Id == Id);
            if (backer == null || creator == null)
                return NotFound();
            var backerFollowCreator = new BackerFollowCreator();
            backerFollowCreator.BackerNavigation = backer;
            backerFollowCreator.ProjectCreatorNavigation = creator;
            return View(backerFollowCreator);
        }

        // POST: Follow/Follow
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Follow(long? Id/*, [Bind("Id,Backer,ProjectCreator")] BackerFollowCreator backerFollowCreator*/)
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if (uId == 0 || Id == null)
                return RedirectToAction("Login", "Users");
            else if (uId == Id)
                return Forbid();
            var backer = _context.User.Include(u => u.UserDetailsNavigation).FirstOrDefault(u => u.Id == uId);
            var creator = _context.User.Include(u => u.UserDetailsNavigation).FirstOrDefault(u => u.Id == Id);
            if (backer == null || creator == null)
                return NotFound();
            BackerFollowCreator backerFollowCreator = new BackerFollowCreator();
            if (ConectionExist(backer.Id, creator.Id))
                return BadRequest();
            _context.User.Update(creator);
            backerFollowCreator.Backer = backer.Id;
            backerFollowCreator.ProjectCreator = creator.Id;
            if (ModelState.IsValid)
            {
                _context.Add(backerFollowCreator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(backerFollowCreator);
        }

        private bool ConectionExist(long backer, long creator)
        {
            return _context.BackerFollowCreator.Any(e => e.Backer == backer && e.ProjectCreator==creator);
        }

        private bool BackerFollowCreatorExists(long id)
        {
            return _context.BackerFollowCreator.Any(e => e.Id == id);
        }
    }
}
