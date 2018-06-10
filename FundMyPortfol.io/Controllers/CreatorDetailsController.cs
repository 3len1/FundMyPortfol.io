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
    public class CreatorDetailsController : Controller
    {
        private readonly PortofolioContext _context;

        public CreatorDetailsController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: CreatorDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.CreatorDetails.ToListAsync());
        }

        // GET: CreatorDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creatorDetails = await _context.CreatorDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creatorDetails == null)
            {
                return NotFound();
            }

            return View(creatorDetails);
        }

        // GET: CreatorDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreatorDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,LastName,FirstName,Country,Town,Street,PostalCode,PhoneNumber")] CreatorDetails creatorDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creatorDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creatorDetails);
        }

        // GET: CreatorDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creatorDetails = await _context.CreatorDetails.FindAsync(id);
            if (creatorDetails == null)
            {
                return NotFound();
            }
            return View(creatorDetails);
        }

        // POST: CreatorDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CreatedDate,LastName,FirstName,Country,Town,Street,PostalCode,PhoneNumber")] CreatorDetails creatorDetails)
        {
            if (id != creatorDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creatorDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreatorDetailsExists(creatorDetails.Id))
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
            return View(creatorDetails);
        }

        // GET: CreatorDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creatorDetails = await _context.CreatorDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creatorDetails == null)
            {
                return NotFound();
            }

            return View(creatorDetails);
        }

        // POST: CreatorDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var creatorDetails = await _context.CreatorDetails.FindAsync(id);
            _context.CreatorDetails.Remove(creatorDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreatorDetailsExists(long id)
        {
            return _context.CreatorDetails.Any(e => e.Id == id);
        }
    }
}
