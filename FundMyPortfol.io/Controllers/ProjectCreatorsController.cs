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
    public class ProjectCreatorsController : Controller
    {
        private readonly PortofolioContext _context;

        public ProjectCreatorsController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: ProjectCreators
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.ProjectCreator.Include(p => p.CreatorDetails);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: ProjectCreators/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCreator = await _context.ProjectCreator
                .Include(p => p.CreatorDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectCreator == null)
            {
                return NotFound();
            }

            return View(projectCreator);
        }

        // GET: ProjectCreators/Create
        public IActionResult Create()
        {
            ViewData["CreatorDetailsId"] = new SelectList(_context.CreatorDetails, "Id", "FirstName");
            return View();
        }

        // POST: ProjectCreators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,BrandName,Link,ProfileImage,ProjectCounter,Followers,BirthDay,About,CreatorDetailsId")] ProjectCreator projectCreator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectCreator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorDetailsId"] = new SelectList(_context.CreatorDetails, "Id", "FirstName", projectCreator.CreatorDetailsId);
            return View(projectCreator);
        }

        // GET: ProjectCreators/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCreator = await _context.ProjectCreator.FindAsync(id);
            if (projectCreator == null)
            {
                return NotFound();
            }
            ViewData["CreatorDetailsId"] = new SelectList(_context.CreatorDetails, "Id", "FirstName", projectCreator.CreatorDetailsId);
            return View(projectCreator);
        }

        // POST: ProjectCreators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CreatedDate,BrandName,Link,ProfileImage,ProjectCounter,Followers,BirthDay,About,CreatorDetailsId")] ProjectCreator projectCreator)
        {
            if (id != projectCreator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectCreator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectCreatorExists(projectCreator.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorDetailsId"] = new SelectList(_context.CreatorDetails, "Id", "FirstName", projectCreator.CreatorDetailsId);
            return View(projectCreator);
        }

        // GET: ProjectCreators/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCreator = await _context.ProjectCreator
                .Include(p => p.CreatorDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectCreator == null)
            {
                return NotFound();
            }

            return View(projectCreator);
        }

        // POST: ProjectCreators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var projectCreator = await _context.ProjectCreator.FindAsync(id);
            _context.ProjectCreator.Remove(projectCreator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectCreatorExists(long id)
        {
            return _context.ProjectCreator.Any(e => e.Id == id);
        }
    }
}
