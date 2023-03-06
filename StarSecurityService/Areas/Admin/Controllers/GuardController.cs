using Microsoft.AspNetCore.Mvc;
using StarSecurityService.Components;
using StarSecurityService.Data;
using StarSecurityService.Extentions;
using StarSecurityService.Models;
using X.PagedList;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarSecurityService.Models.ViewModels;

namespace StarSecurityService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuardController : Controller
    {
        
        [SessionFilter]

        private readonly StarSecurityServiceDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GuardController(StarSecurityServiceDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment=hostEnvironment;
        }
        public ActionResult Index (string search,int? page, string sortOrder, string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewData["CurrentFilter"] = search;

            var models = from g in _context.Guards select g;
            switch (sortOrder)
            {
                case "name_desc":
                    models = _context.Guards.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    models = _context.Guards.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    models = _context.Guards.OrderByDescending(s => s.CreatedDate);
                    break;
                case "Id_desc":
                    models = _context.Guards.OrderByDescending(s => s.GuardId);
                    break;
                default:
                    models = _context.Guards.OrderBy(s => s.GuardId);
                    break;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
           /* var models = from g in _context.Guards select g;*/
            return View(models.AsNoTracking().Include(g => g.Service).Where(g =>
                g.CardId.Contains(search) || g.FirstName.Contains(search) || g.LastName.Contains(search) ||
                search == null).ToPagedList(pageNumber, pageSize));
        }

        [SessionFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Guards == null)
            {
                return NotFound();
            }

            var guard = await _context.Guards
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.GuardId == id);
            if (guard == null)
            {
                return NotFound();
            }

            return View(guard);
        }

        [SessionFilter]
        public IActionResult Create()
        {
            var services = new ServiceComponents().ListAll();
            SelectList serviceList = new SelectList(services, "ServiceId", "ServiceName");
            ViewBag.Service = serviceList;
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuardId,FirstName,LastName,ServiceId,Phone,Height,Weight,Status,Avatar, ImageFile, CardId")] Guard guard)
        {
            if (ModelState.IsValid)
            {
                ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", guard.ServiceId);
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(guard.ImageFile.FileName);
                string extension = Path.GetExtension(guard.ImageFile.FileName);
                guard.Avatar=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/adminassests/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await guard.ImageFile.CopyToAsync(fileStream);
                }
                _context.Add(guard);
                await _context.SaveChangesAsync();
            }
            /*ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", guard.ServiceId);*/
            return View(guard);
        }

        [SessionFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Guards == null)
            {
                return NotFound();
            }

            var guard = await _context.Guards.FindAsync(id);
            if (guard == null)
            {
                return NotFound();
            }
            var services = new ServiceComponents().ListAll();
            SelectList serviceList = new SelectList(services, "ServiceId", "ServiceName");
            ViewBag.Service = serviceList;
            return View(guard);
        }
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuardId,FirstName,LastName,ServiceId,Phone,Height,Weight,Status,Avatar, ImageFile, CardId")] Guard guard)
        {
            if (id != guard.GuardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(guard.ImageFile.FileName);
                    string extension = Path.GetExtension(guard.ImageFile.FileName);
                    guard.Avatar=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/adminassests/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await guard.ImageFile.CopyToAsync(fileStream);
                    }
                    _context.Update(guard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuardExists(guard.GuardId))
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
            return View(guard);
        }

        [SessionFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Guards == null)
            {
                return NotFound();
            }

            var guard = await _context.Guards
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.GuardId == id);
            if (guard == null)
            {
                return NotFound();
            }

            return View(guard);
        }

        [HttpPost, ActionName("Delete")]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Guards == null)
            {
                return Problem("Entity set 'StarSecurityServiceDBContext.Guards'  is null.");
            }
            var guard = await _context.Guards.FindAsync(id);
            //delete image form wwwroot
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/adminassests/Image/", guard.Avatar);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            if (guard != null)
            {
                _context.Guards.Remove(guard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuardExists(int id)
        {
            return (_context.Guards?.Any(e => e.GuardId == id)).GetValueOrDefault();
        }
    }
}
