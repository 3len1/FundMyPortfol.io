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
    public class BackersController : Controller
    {
        private readonly PortofolioContext _context;

        public BackersController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: Backers
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.Backer.Include(b => b.BackerDetails);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Backers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backer = await _context.Backer
                .Include(b => b.BackerDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backer == null)
            {
                return NotFound();
            }

            return View(backer);
        }

        // GET: Backers/Create
        public IActionResult Create()
        {
            ViewData["BackerDetailsId"] = new SelectList(_context.BackerDetails, "Id", "FirstName");
            return View();
        }

        // POST: Backers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,BackerDetailsId")] Backer backer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(backer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BackerDetailsId"] = new SelectList(_context.BackerDetails, "Id", "FirstName", backer.BackerDetailsId);
            return View(backer);
        }

        // GET: Backers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backer = await _context.Backer.FindAsync(id);
            if (backer == null)
            {
                return NotFound();
            }
            ViewData["BackerDetailsId"] = new SelectList(_context.BackerDetails, "Id", "FirstName", backer.BackerDetailsId);
            return View(backer);
        }

        // POST: Backers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CreatedDate,BackerDetailsId")] Backer backer)
        {
            if (id != backer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(backer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackerExists(backer.Id))
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
            ViewData["BackerDetailsId"] = new SelectList(_context.BackerDetails, "Id", "FirstName", backer.BackerDetailsId);
            return View(backer);
        }

        // GET: Backers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backer = await _context.Backer
                .Include(b => b.BackerDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backer == null)
            {
                return NotFound();
            }

            return View(backer);
        }

        // POST: Backers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var backer = await _context.Backer.FindAsync(id);
            _context.Backer.Remove(backer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackerExists(long id)
        {
            return _context.Backer.Any(e => e.Id == id);
        }
    }
}
