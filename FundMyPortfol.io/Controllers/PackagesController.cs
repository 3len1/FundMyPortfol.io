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
    public class PackagesController : Controller
    {
        private readonly PortofolioContext _context;

        public PackagesController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: Packages
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.Package.Include(p => p.ProjectNavigation).OrderBy(p => p.ProjectNavigation.Title);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Packages/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return BadRequest();
            var package = await _context.Package
                .Include(p => p.ProjectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
                return NotFound();

            return View(package);
        }

        // GET: Packages/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Project"] = new SelectList(_context.Project.Where(p=> p.ProjectCtrator == LoggedUser()), "Id", "Title");
            return View();
        }

        // POST: Packages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,PackageName,PledgeAmount,PackageLeft,DeliveryDate,Description,Project")] Package package)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _context.Add(package);
            await _context.SaveChangesAsync();
            return Json(new
            {
                RedirectUrl = Url.Action("details", "packages", new { id = package.Id })
            });

        }

        // GET: Packages/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return BadRequest();
            var package = await _context.Package.FindAsync(id);
            if (package == null)
                return NotFound();
            ViewData["Project"] = new SelectList(_context.Project.Where(p => p.ProjectCtrator == LoggedUser()), "Id", "Title");
            return View(package);
        }

        // POST: Packages/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CreatedDate,PackageName,PledgeAmount,TimesSelected,PackageLeft,DeliveryDate,Description,Project")] Package package)
        {
            if (id != package.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _context.Update(package);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(package.Id))
                    return NotFound();
                else
                    throw;
            }
            return Json(new
            {
                RedirectUrl = Url.Action("details", "packages", new { id = package.Id })
            });

        }

        // GET: Packages/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return BadRequest();

            var package = await _context.Package
                .Include(p => p.ProjectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
                return NotFound();

            return View(package);
        }

        // POST: Packages/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var package = await _context.Package.FindAsync(id);
            _context.Package.Remove(package);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(long id)
        {
            return _context.Package.Any(e => e.Id == id);
        }

        private long LoggedUser()
        {
            string logeduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            long.TryParse(logeduser.ToString(), out long uId);
            return uId;
        }
    }
}