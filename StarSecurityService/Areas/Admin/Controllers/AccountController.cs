using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StarSecurityService.Components;
using StarSecurityService.Data;
using StarSecurityService.Extentions;
using StarSecurityService.Models;
using X.PagedList;

namespace StarSecurityService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        [SessionFilter]

        private readonly StarSecurityServiceDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AccountController(StarSecurityServiceDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment=hostEnvironment;
        }
        public ActionResult Index(string search, int? page, string sortOrder, string currentFilter)
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

            var models = from a in _context.Accounts select a;
            switch (sortOrder)
            {
                case "name_desc":
                    models = _context.Accounts.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    models = _context.Accounts.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    models = _context.Accounts.OrderByDescending(s => s.CreatedDate);
                    break;
                case "Id_desc":
                    models = _context.Accounts.OrderByDescending(s => s.AccountId);
                    break;
                default:
                    models = _context.Accounts.OrderBy(s => s.AccountId);
                    break;
            }
            var pageNumber = page ?? 1;
            var pageSize = 5;
            /* var models = from g in _context.Guards select g;*/
            return View(models.AsNoTracking().Include(g => g.Role).Where(g =>
                g.CardId.Contains(search) || g.FirstName.Contains(search) || g.LastName.Contains(search) ||
                g.Role.RoleName.Contains(search) ||
                search == null).ToPagedList(pageNumber, pageSize));
        }

        [SessionFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(b => b.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [SessionFilter]
        public IActionResult Create()
        {
            var roles = new RoleComponents().ListAll();
            SelectList roleList = new SelectList(roles, "RoleId", "RoleName");
            ViewBag.Role = roleList;
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,FirstName,LastName,RoleId,Phone, Email,Avatar, ImageFile, CardId, Password")] Account account)
        {
            if (ModelState.IsValid)
            {
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(account.ImageFile.FileName);
                string extension = Path.GetExtension(account.ImageFile.FileName);
                account.Avatar=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/ClientAssets/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await account.ImageFile.CopyToAsync(fileStream);
                }
                account.Password = BCrypt.Net.BCrypt.HashPassword("123456"); // default pass is "123456", user can modify it later
                _context.Add(account);
                await _context.SaveChangesAsync();
            }
            /*ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", guard.ServiceId);*/
            return View(account);
        }

        [SessionFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            var roles = new RoleComponents().ListAll();
            SelectList roleList = new SelectList(roles, "RoleId", "RoleName");
            ViewBag.Role = roleList;
            return View(account);
        }
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,FirstName,LastName,RoleId,Phone, Email,Avatar, ImageFile, CardId")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(account.ImageFile.FileName);
                    string extension = Path.GetExtension(account.ImageFile.FileName);
                    account.Avatar=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/ClientAssets/image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await account.ImageFile.CopyToAsync(fileStream);
                    }
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            return View(account);
        }

        [SessionFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(b => b.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'StarSecurityServiceDBContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            //delete image form wwwroot
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/ClientAssets/image/", account.Avatar);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
