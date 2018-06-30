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
            var portofolioContext = _context.BackerBuyPackage.Include(b => b.BackerNavigation).Include(b => b.PackageNavigation)
                .Include(b => b.PackageNavigation.ProjectNavigation);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Buy/Donates
        public async Task<IActionResult> Donates()
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if (uId == 0)
                return RedirectToAction("Login", "User");
            var portofolioContext = _context.BackerBuyPackage.Include(b => b.BackerNavigation).Include(b => b.PackageNavigation)
                .Include(b => b.PackageNavigation.ProjectNavigation).Where(b=> b.Backer==uId);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Buy/BuyForm
        public IActionResult BuyForm(long? Id)
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if (uId == 0 || Id == null)
                return RedirectToAction("Login", "User");
            var user = _context.User.FirstOrDefault(u => u.Id == uId);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(long? Id, [Bind("Backer,Package,DeliveryDate")] BackerBuyPackage backerBuyPackage)
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if(uId == 0)
                return RedirectToAction("Login", "User");
            var user = _context.User.FirstOrDefault(u => u.Id == uId);
            var package = _context.Package.Include(p => p.ProjectNavigation).FirstOrDefault(p => p.Id == Id);
            if (user == null || package == null)
                return RedirectToAction("Login", "User");
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

        public long LoggedUser()
        {
            string logeduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            long.TryParse(logeduser.ToString(), out long uId);
            return uId;
        }

    }
}
