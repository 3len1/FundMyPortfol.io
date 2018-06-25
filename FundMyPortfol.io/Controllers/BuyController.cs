using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundMyPortfol.io.Models;

namespace FundMyPortfol.io.Controllers
{
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

        // GET: Buy/BuyForm
        public IActionResult BuyForm(long? Id)
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            if (uId == 0 || Id == null)
            {
                return BadRequest();
            }
            var user = _context.User.FirstOrDefault(u => u.Id == uId);
            var package = _context.Package.Include(p => p.ProjectNavigation).FirstOrDefault(p => p.Id == Id);
            if (user == null || package == null)
            {
                return NotFound();
            }
            //package.PackageLeft--;
            if(user.Id == package.ProjectNavigation.ProjectCtrator)
            {
                return Forbid();
            }
            //_context.Package.Update(package);
            var backerBuyPackage = new BackerBuyPackage();
            backerBuyPackage.BackerNavigation = user;
            backerBuyPackage.PackageNavigation = package;
            return View(backerBuyPackage);
        }

        // POST: Buy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(long? Id, [Bind("Backer,Package,DeliveryDate")] BackerBuyPackage backerBuyPackage)
        {
            long uId;
            long.TryParse(HttpContext.Request.Cookies["userId"]?.ToString(), out uId);
            var user = _context.User.FirstOrDefault(u => u.Id == uId);
            var package = _context.Package.Include(p => p.ProjectNavigation).FirstOrDefault(p => p.Id == Id);
            if (user == null || package == null)
            {
                return NotFound();
            }
            package.PackageLeft--;
            if(package.PackageLeft < 0 || package.ProjectNavigation.ExpireDate < DateTime.Now)
            {
                return RedirectToAction(nameof(Index));
            }
            package.ProjectNavigation.MoneyReach = package.ProjectNavigation.MoneyReach + package.PledgeAmount;
            _context.Package.Update(package);
            backerBuyPackage.Backer = user.Id;
            backerBuyPackage.Package = package.Id;
            if (ModelState.IsValid)
            {
                _context.Add(backerBuyPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewData["Backer"] = new SelectList(_context.User, "Id", "Email", backerBuyPackage.Backer);
            ViewData["Package"] = new SelectList(_context.Package, "Id", "Description", backerBuyPackage.Package);
            return View(backerBuyPackage);
        }

        private bool BackerBuyPackageExists(long id)
        {
            return _context.BackerBuyPackage.Any(e => e.Backer == id);
        }
    }
}
