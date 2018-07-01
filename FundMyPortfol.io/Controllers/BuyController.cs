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
    public class BuyController : Controller
    {
        private readonly PortofolioContext _context;

        public BuyController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: Buy
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.BackerBuyPackage.Include(b => b.BackerNavigation).Include(b => b.BackerNavigation.UserDetailsNavigation)
                .Include(b => b.PackageNavigation).Include(b => b.PackageNavigation.ProjectNavigation)
                .OrderBy(p => p.PackageNavigation.Project).ThenBy(p=> p.Package);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Buy/Donates
        [Authorize]
        public async Task<IActionResult> Donates()
        {
            var portofolioContext = _context.BackerBuyPackage.Include(b => b.BackerNavigation).Include(b => b.BackerNavigation.UserDetailsNavigation)
                .Include(b => b.PackageNavigation).Include(b => b.PackageNavigation.ProjectNavigation)
                .Where(b=> b.Backer==LoggedUser()).OrderBy(p => p.PackageNavigation.Project).ThenBy(p => p.Package); 
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Buy/BuyForm
        [Authorize]
        public IActionResult BuyForm(long? Id)
        {
           
            var user = _context.User.FirstOrDefault(u => u.Id == LoggedUser());
            var package = _context.Package.Include(p => p.ProjectNavigation).FirstOrDefault(p => p.Id == Id);
            if (user == null || package == null)
                return NotFound();
            if(user.Id == package.ProjectNavigation.ProjectCtrator)
                return Forbid();
            var backerBuyPackage = new BackerBuyPackage();
            backerBuyPackage.BackerNavigation = user;
            backerBuyPackage.PackageNavigation = package;
            return View(backerBuyPackage);
        }

        // POST: Buy/Buy
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(long? Id, [Bind("Backer,Package,DeliveryDate")] BackerBuyPackage backerBuyPackage)
        { 
            var user = _context.User.FirstOrDefault(u => u.Id == LoggedUser());
            var package = _context.Package.Include(p => p.ProjectNavigation).FirstOrDefault(p => p.Id == Id);
            if (user == null || package == null)
                return RedirectToAction("Index");
            package.PackageLeft--;
            if(package.PackageLeft < 0 || package.ProjectNavigation.ExpireDate < DateTime.Now)
                return RedirectToAction(nameof(Index));
            package.ProjectNavigation.MoneyReach = package.ProjectNavigation.MoneyReach + package.PledgeAmount;
            _context.Package.Update(package);
            backerBuyPackage.Backer = user.Id;
            backerBuyPackage.Package = package.Id;
            if (!ModelState.IsValid)
                return BadRequest();
            _context.Add(backerBuyPackage);
            await _context.SaveChangesAsync();
            return Json(new
            {
                RedirectUrl = Url.Action("donates", "buy")
            });
        }

        private bool BackerBuyPackageExists(long id)
        {
            return _context.BackerBuyPackage.Any(e => e.Backer == id);
        }

        private long LoggedUser()
        {
            string logeduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            long.TryParse(logeduser.ToString(), out long uId);
            return uId;
        }

    }
}
