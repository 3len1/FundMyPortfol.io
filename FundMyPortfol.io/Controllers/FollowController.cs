using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundMyPortfol.io.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.BackerFollowCreator.Include(u => u.BackerNavigation).Include(u => u.ProjectCreatorNavigation)
                .Include(d => d.BackerNavigation.UserDetailsNavigation).Include(d => d.ProjectCreatorNavigation.UserDetailsNavigation)
                .Where(b => b.Backer == LoggedUser());
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Follow/Followers
        [Authorize]
        public async Task<IActionResult> Followers()
        {
            var portofolioContext = _context.BackerFollowCreator.Include(u => u.BackerNavigation).Include(u => u.ProjectCreatorNavigation)
                .Include(d => d.BackerNavigation.UserDetailsNavigation).Include(d => d.ProjectCreatorNavigation.UserDetailsNavigation)
                .Where(b => b.ProjectCreator == LoggedUser());
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Follow/FollowForm
        [Authorize]
        public IActionResult FollowForm(long? Id)
        {
            if (LoggedUser() == Id)
                return Forbid();
            var backer = _context.User.Include(u => u.UserDetailsNavigation).FirstOrDefault(u => u.Id == LoggedUser());
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Follow(long? Id/*, [Bind("Id,Backer,ProjectCreator")] BackerFollowCreator backerFollowCreator*/)
        {
            if (Id== null)
                return Forbid();
            else if (LoggedUser() == Id)
                return Forbid();
            var backer = _context.User.Include(u => u.UserDetailsNavigation).FirstOrDefault(u => u.Id == LoggedUser());
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
            return _context.BackerFollowCreator.Any(e => e.Backer == backer && e.ProjectCreator == creator);
        }

        private bool BackerFollowCreatorExists(long id)
        {
            return _context.BackerFollowCreator.Any(e => e.Id == id);
        }

        public long LoggedUser()
        {
            string logeduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            long.TryParse(logeduser.ToString(), out long uId);
            return uId;
        }
    }
}
