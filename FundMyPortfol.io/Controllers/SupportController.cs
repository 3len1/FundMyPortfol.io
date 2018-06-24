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
    public class SupportController : Controller
    {
        private readonly PortofolioContext _context;

        public SupportController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: Support
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.BackerBuyPackage.Include(b => b.BackerNavigation).Include(b => b.PackageNavigation);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Support/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backerBuyPackage = await _context.BackerBuyPackage
                .Include(b => b.BackerNavigation)
                .Include(b => b.PackageNavigation)
                .FirstOrDefaultAsync(m => m.Backer == id);
            if (backerBuyPackage == null)
            {
                return NotFound();
            }

            return View(backerBuyPackage);
        }

        // GET: Support/Create
        public IActionResult Create()
        {
            ViewData["Backer"] = new SelectList(_context.User, "Id", "Email");
            ViewData["Package"] = new SelectList(_context.Package, "Id", "Description");
            return View();
        }

        // POST: Support/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Backer,Package,DeliveryDate")] BackerBuyPackage backerBuyPackage)
        {
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

        // GET: Support/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backerBuyPackage = await _context.BackerBuyPackage
                .Include(b => b.BackerNavigation)
                .Include(b => b.PackageNavigation)
                .FirstOrDefaultAsync(m => m.Backer == id);
            if (backerBuyPackage == null)
            {
                return NotFound();
            }

            return View(backerBuyPackage);
        }

        // POST: Support/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var backerBuyPackage = await _context.BackerBuyPackage.FindAsync(id);
            _context.BackerBuyPackage.Remove(backerBuyPackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackerBuyPackageExists(long id)
        {
            return _context.BackerBuyPackage.Any(e => e.Backer == id);
        }
    }
}
