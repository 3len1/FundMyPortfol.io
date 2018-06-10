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
    public class BackerDetailsController : Controller
    {
        private readonly PortofolioContext _context;

        public BackerDetailsController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: BackerDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.BackerDetails.ToListAsync());
        }

        // GET: BackerDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backerDetails = await _context.BackerDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backerDetails == null)
            {
                return NotFound();
            }

            return View(backerDetails);
        }

        // GET: BackerDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,LastName,FirstName,Country,Town,Street,PostalCode,PhoneNumber")] BackerDetails backerDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(backerDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(backerDetails);
        }

        // GET: BackerDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backerDetails = await _context.BackerDetails.FindAsync(id);
            if (backerDetails == null)
            {
                return NotFound();
            }
            return View(backerDetails);
        }

        // POST: BackerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CreatedDate,LastName,FirstName,Country,Town,Street,PostalCode,PhoneNumber")] BackerDetails backerDetails)
        {
            if (id != backerDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(backerDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackerDetailsExists(backerDetails.Id))
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
            return View(backerDetails);
        }

        // GET: BackerDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backerDetails = await _context.BackerDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backerDetails == null)
            {
                return NotFound();
            }

            return View(backerDetails);
        }

        // POST: BackerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var backerDetails = await _context.BackerDetails.FindAsync(id);
            _context.BackerDetails.Remove(backerDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackerDetailsExists(long id)
        {
            return _context.BackerDetails.Any(e => e.Id == id);
        }
    }
}
