using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundMyPortfol.io.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FundMyPortfol.io.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly PortofolioContext _context;
        private readonly IHostingEnvironment _environment;

        public ProjectsController(PortofolioContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Projects
        [AllowAnonymous]
        public IActionResult Index(Project.Category category)
        {
            var enumValues = Enum.GetValues(typeof(Project.Category));
            System.Linq.IQueryable result = null;
            if (category == 0)
                result = _context.Project.Include(p => p.ProjectCtratorNavigation)
                    .Include(p => p.ProjectCtratorNavigation.UserDetailsNavigation).OrderBy(p => p.ProjectCtrator);
            else
                result = _context.Project.Include(p => p.ProjectCtratorNavigation).
                    Include(p => p.ProjectCtratorNavigation.UserDetailsNavigation).
                    Where(p => p.ProjectCategory == category).OrderBy(p=> p.ProjectCtrator);
            ViewData["ddProjectCategory"] = category;
            return View(result);
        }

        // GET: Projects/Funded
        [AllowAnonymous]
        public async Task<IActionResult> Funded()
        {
            var portofolioContext = _context.Project.Include(p => p.ProjectCtratorNavigation)
                                    .Include(p=> p.ProjectCtratorNavigation.UserDetailsNavigation).Where(p => p.MoneyReach>0);
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Projects/Available
        [AllowAnonymous]
        public async Task<IActionResult> Available()
        {
            var portofolioContext = _context.Project.Include(p => p.ProjectCtratorNavigation)
                                    .Include(p => p.ProjectCtratorNavigation.UserDetailsNavigation)
                                    .Where(p => p.ExpireDate > DateTime.Now );
            return View(await portofolioContext.ToListAsync());
        }

        // GET: Projects/Creator
        [Authorize]
        public async Task<IActionResult> Creator()
        {
            var portofolioContext = _context.Project.Include(p => p.ProjectCtratorNavigation)
                                    .Include(p => p.ProjectCtratorNavigation.UserDetailsNavigation)
                                    .Where(p => p.ProjectCtrator == LoggedUser());
            return View(await portofolioContext.ToListAsync());
        }

        
        // GET: Projects/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return BadRequest();
            var project = await _context.Project
                .Include(p => p.ProjectCtratorNavigation).Include(u => u.ProjectCtratorNavigation.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
                return NotFound();
            var packages = _context.Package.Where(p => p.Project == id);
            ViewBag.data = packages.ToList();

            return View(project);
        }


        // GET: Projects/Create
        [Authorize]
        public IActionResult Create()
        {
            var categories = from Project.Category c in Enum.GetValues(typeof(Project.Category))
                             select c.ToString();
            ViewData["CategoryBag"] = new SelectList(categories);
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,ProjectCategory,Title,Likes,PablishDate,ExpireDate,MoneyGoal,MoneyReach,Description,ProjectCtrator")] Project project)
        {
            var httpFiles = HttpContext.Request.Form.Files;
            var image = AddMediaFiles(project, LoggedUser().ToString(), httpFiles);

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == LoggedUser());
            project.ProjectImage = image;
            project.ProjectCtrator = user.Id;
            project.ProjectCtratorNavigation = user;
            _context.Add(project);
            await _context.SaveChangesAsync();
            return Json(new
            {
                RedirectUrl = Url.Action("details", "projects", new { id = project.Id })
            });
        }


        // POST: Projects/Like/5
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Like(long? id)
        {
            if (id == null)
                return BadRequest();
            var project = await _context.Project.FindAsync(id);
            if (project == null)
                return NotFound();
            try
            {
                project.Likes++;
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction("Details", project);   
        }

        // GET: Projects/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return BadRequest();

            var project = await _context.Project.FindAsync(id);
            if (project == null)
                return NotFound();
            var categories = from Project.Category c in Enum.GetValues(typeof(Project.Category))
                             select c.ToString();
            ViewData["CategoryBag"] = new SelectList(categories);
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ProjectCategory,Title,PablishDate,ExpireDate,MoneyGoal,Description")] Project updateProject)
        {
            if (id != updateProject.Id)
                return BadRequest();

            var httpFiles = HttpContext.Request.Form.Files;
            var image = AddMediaFiles(updateProject, LoggedUser().ToString(), httpFiles);
            if (!ModelState.IsValid)
                return BadRequest();
            var project = await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
            project.Title = updateProject.Title;
            project.ProjectCategory = updateProject.ProjectCategory;
            project.PablishDate = updateProject.PablishDate;
            project.ExpireDate = updateProject.ExpireDate;
            project.MoneyGoal = updateProject.MoneyGoal;
            project.Description = updateProject.Description;
            if(image!=null)
                project.ProjectImage = image;
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
                    return NotFound();
                else
                    throw;
            }
            return Json(new
            {
                RedirectUrl = Url.Action("details", "projects", new { id = project.Id })
            });

        }

        // GET: Projects/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return BadRequest();

            var project = await _context.Project
                .Include(p => p.ProjectCtratorNavigation)
                .Include(p => p.ProjectCtratorNavigation.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var project = await _context.Project.FindAsync(id);
            var user = await _context.User.FindAsync(project.ProjectCtrator);
            _context.User.Update(user);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(long id)
        {
            return _context.Project.Any(e => e.Id == id);
        }


        private string AddMediaFiles(Project project, string userId, IFormFileCollection httpFiles)
        {
            if (httpFiles.Count() > 0)
            {
                string pathToDir = string.Empty;
                var photoName = "";
                var mediaFile = httpFiles;

                var folder = Path.Combine(_environment.WebRootPath, "media");
                var createdDirectory = Directory.CreateDirectory(folder + "\\" + userId + "\\" + $"{project.Title.ToLower()}");
                
                foreach (var photo in mediaFile)
                {
                    if (photo.Length > 0)
                    {
                        photoName = httpFiles[0].GetFilename();
                        pathToDir = createdDirectory + "\\" + photoName;
                    }
                    using (FileStream fs = new FileStream(pathToDir, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        photo.CopyTo(fs);
                        fs.Flush();
                    }
                }
                return "\\media\\"+ userId +"\\" + project.Title.ToLower()+"\\"+photoName;
            }
            return null;
        }

        private long LoggedUser()
        {
            string logeduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            long.TryParse(logeduser.ToString(), out long uId);
            return uId;
        }
    }
}
