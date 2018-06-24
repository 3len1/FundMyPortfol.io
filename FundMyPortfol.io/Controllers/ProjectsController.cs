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
    public class ProjectsController : Controller
    {
        private readonly PortofolioContext _context;

        public ProjectsController(PortofolioContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var portofolioContext = _context.Project.Include(p => p.ProjectCtratorNavigation);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.ProjectCtratorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["ProjectCtrator"] = new SelectList(_context.User, "Id", "Email");
            var categories = from Project.Category c in Enum.GetValues(typeof(Project.Category))
                             select c.ToString();
            ViewData["CategoryBag"] = new SelectList(categories);
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectCategory,Title,ProjectImage,Likes,PablishDate,ExpireDate,MoneyGoal,MoneyReach,Description,ProjectCtrator")] Project project)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User.FirstOrDefaultAsync(m => m.Id == project.ProjectCtrator);
                user.ProjectCounter ++;
                _context.User.Update(user);
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectCtrator"] = new SelectList(_context.User, "Id", "Email", project.ProjectCtrator);
            return View(project);
        }


        // POST: Projects/Like/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            try
            {
                project.Likes++;
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //ViewData["ProjectCtrator"] = new SelectList(_context.User, "Id", "Email", project.ProjectCtrator);
            //var categories = from Project.Category c in Enum.GetValues(typeof(Project.Category))
            //                 select c.ToString();
            //ViewData["CategoryBag"] = new SelectList(categories);
            return RedirectToAction("Details", project);   
        }
        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectCtrator"] = new SelectList(_context.User, "Id", "Email", project.ProjectCtrator);
            var categories = from Project.Category c in Enum.GetValues(typeof(Project.Category))
                             select c.ToString();
            ViewData["CategoryBag"] = new SelectList(categories);
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ProjectCategory,Title,ProjectImage,PablishDate,ExpireDate,MoneyGoal,Description")] Project updateProject)
        {
            if (id != updateProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var project = await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
                project.Title = updateProject.Title;
                project.ProjectCategory = updateProject.ProjectCategory;
                project.ProjectImage = updateProject.ProjectImage;
                project.PablishDate = updateProject.PablishDate;
                project.ExpireDate = updateProject.ExpireDate;
                project.MoneyGoal = updateProject.MoneyGoal;
                project.Description = updateProject.Description;
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            //ViewData["ProjectCtrator"] = new SelectList(_context.User, "Id", "Email", updateProject.ProjectCtrator);
            return View(updateProject);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.ProjectCtratorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var project = await _context.Project.FindAsync(id);
            var user = await _context.User.FindAsync(project.ProjectCtrator);
            user.ProjectCounter--;
            _context.User.Update(user);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(long id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
