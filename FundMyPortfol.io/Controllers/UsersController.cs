using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FundMyPortfol.io.Models;
using FundMyPortfol.io.ViewModels;
using FundMyPortfol.io.Converter;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FundMyPortfol.io.Controllers
{
    
    public class UsersController : Controller
    {
        private UsersConverter _usersConverter = new UsersConverter();
        private readonly PortofolioContext _context;
        private readonly IHostingEnvironment _environment;

        public UsersController(PortofolioContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var portofolioContext = await _context.User.Include(u => u.UserDetailsNavigation).Include(p => p.Project).ToListAsync();
            var users = new List<UserView>();
            foreach(var user in portofolioContext){
                UserView uv = new UserView();
                uv.Id = user.Id;
                uv.FirstName = user.UserDetailsNavigation.FirstName;
                uv.LastName = user.UserDetailsNavigation.LastName;
                uv.ProjectCounter = user.Project.Where(p => p.ProjectCtrator == user.Id).Count();
                uv.Followers = _context.BackerFollowCreator.Where(b => b.ProjectCreator == user.Id).Count();
                users.Add(uv);
            }
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return BadRequest();

            var user = await _context.User
                .Include(u => u.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
                return NotFound();

            UserView userView = _usersConverter.UsertoUserViewConverter(user);
            userView.ProjectCounter = _context.Project.Where(p => p.ProjectCtrator == id).Count();
            userView.Followers = _context.BackerFollowCreator.Where(b => b.ProjectCreator == id).Count();
            var projects = _context.Project.Where(p => p.ProjectCtrator == id);
            userView.Project = projects.ToList();

            return View(userView);
        }

        // GET: Users/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return BadRequest();
            if (LoggedUser() != id)
                return Forbid();
            var user = await _context.User
                .Include(u => u.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
                return NotFound();
            UserView userView = _usersConverter.UsertoUserViewConverter(user);
            return View(userView);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Email,Password,ProjectCounter,Followers,LastName,FirstName,Country,Town,Street,PostalCode,PhoneNumber,ProfileImage")] UserView userView)
        {
            if (id != userView.Id)
                return BadRequest();
            if (LoggedUser() != id)
                return Forbid();
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            var retriveUser = _usersConverter.UserViewtoUserConverter(userView);
            var httpFiles = HttpContext.Request.Form.Files;
            var image = AddMediaFiles(LoggedUser().ToString(), httpFiles);
            UserDetails userDetails = _usersConverter.UserViewtoUserDetailsConverter(userView);
            userDetails.Id = user.UserDetails;
            userDetails.ProfileImage = image;
            try
            {
                _context.Update(userDetails);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                    return NotFound();
                else
                    throw;
            }
            return Json(new
            {
                RedirectUrl = Url.Action("details", "users", new { id = user.Id })
            });
        }

        // GET: Users/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return NotFound();
            if (LoggedUser() != id)
                return Forbid();
            var user = await _context.User
                .Include(u => u.UserDetailsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
                return NotFound();
            
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (LoggedUser() != id)
                return Forbid();
            var user = await _context.User.FindAsync(id);
            var userDetails = await _context.UserDetails.FindAsync(user.UserDetails);
            _context.User.Remove(user);
            _context.UserDetails.Remove(userDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        private string AddMediaFiles(string userId, IFormFileCollection httpFiles)
        {
            if (httpFiles.Count() > 0)
            {
                string pathToDir = string.Empty;
                var photoName = "";
                var mediaFile = httpFiles;

                var folder = Path.Combine(_environment.WebRootPath, "media");
                var createdDirectory = Directory.CreateDirectory(folder + "\\" + userId);
                
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
                return "\\media\\" + userId + "\\" + photoName;

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
