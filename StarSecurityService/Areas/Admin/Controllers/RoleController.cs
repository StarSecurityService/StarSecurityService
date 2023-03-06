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
    public class RoleController : Controller
    {
        [SessionFilter]

        private readonly StarSecurityServiceDbContext _context;
        public RoleController(StarSecurityServiceDbContext context)
        {
            _context = context;
        }
        public ActionResult Index(string search, int? page, string sortOrder, string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var models = from r in _context.Roles select r;
            switch (sortOrder)
            {
                case "name_desc":
                    models = _context.Roles.OrderByDescending(s => s.RoleName);
                    break;
                case "Id_desc":
                    models = _context.Roles.OrderByDescending(s => s.RoleId);
                    break;
                default:
                    models = _context.Roles.OrderBy(s => s.RoleId);
                    break;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            /* var models = from g in _context.Guards select g;*/
            return View(models.Where(g =>
               g.RoleName.Contains(search) ||
                search == null).ToPagedList(pageNumber, pageSize));
        }

        [SessionFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
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
        public async Task<IActionResult> Create([Bind("RoleId, RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(role);
                await _context.SaveChangesAsync();
            }
            
            return View(role);
        }

        [SessionFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            
            return View(role);
        }
        [HttpPost]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId, RoleName")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
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
            return View(role);
        }

        [SessionFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'StarSecurityServiceDBContext.Roles'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
