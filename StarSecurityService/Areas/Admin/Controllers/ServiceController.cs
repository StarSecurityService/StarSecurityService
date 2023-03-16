using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityService.Components;
using StarSecurityService.Data;
using StarSecurityService.Extentions;
using StarSecurityService.Models;
using X.PagedList;

namespace StarSecurityService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        [SessionFilter]

        private readonly StarSecurityServiceDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ServiceController(StarSecurityServiceDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment=hostEnvironment;
        }
        public ActionResult Index(string search, int? page, string sortOrder, string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
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

            var models = from s in _context.Services select s;
            switch (sortOrder)
            {
                case "name_desc":
                    models = _context.Services.OrderByDescending(s => s.ServiceName);
                    break;
                case "Price":
                    models = _context.Services.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    models = _context.Services.OrderByDescending(s => s.Price);
                    break;
                case "Id_desc":
                    models = _context.Services.OrderByDescending(s => s.ServiceId);
                    break;
                default:
                    models = _context.Services.OrderBy(s => s.ServiceId);
                    break;
            }
            var pageNumber = page ?? 1;
            var pageSize = 5;
            /* var models = from g in _context.Guards select g;*/
            return View(models.Where(s =>
                s.ServiceName.Contains(search) ||
                search == null).ToPagedList(pageNumber, pageSize));
        }

        [SessionFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [SessionFilter]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId, ServiceName, Price, Image, ImageFile, Description")] Service service)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(service.ImageFile.FileName);
                string extension = Path.GetExtension(service.ImageFile.FileName);
                service.Image=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/ClientAssets/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await service.ImageFile.CopyToAsync(fileStream);
                }
                _context.Add(service);
                await _context.SaveChangesAsync();
            }
            return View(service);
        }

        [SessionFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId, ServiceName, Price, Image, ImageFile, Description")] Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(service.ImageFile.FileName);
                    string extension = Path.GetExtension(service.ImageFile.FileName);
                    service.Image=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/ClientAssets/image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await service.ImageFile.CopyToAsync(fileStream);
                    }
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId))
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
            return View(service);
        }

        [SessionFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'StarSecurityServiceDBContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            //delete image form wwwroot
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/ClientAssets/image/", service.Image);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            if (service != null)
            {
                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return (_context.Services?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
