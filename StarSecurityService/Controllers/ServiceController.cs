using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityService.Components;
using StarSecurityService.Data;
using StarSecurityService.Extentions;
using StarSecurityService.Models.ViewModels;
using StarSecurityService.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace StarSecurityService.Controllers
{
    public class ServiceController : Controller
    {
        private readonly StarSecurityServiceDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private INotyfService _notyfService;

        public ServiceController(StarSecurityServiceDbContext context, IWebHostEnvironment hostEnvironment, INotyfService notifyService)
        {
            _context = context;
            _hostEnvironment=hostEnvironment;
            _notyfService = notifyService;
        }

        public ActionResult Index(string search, int? page, string currentFilter)
        {
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
            var pageNumber = page ?? 1;
            var pageSize = 6;
            /* var models = from g in _context.Guards select g;*/
            return View(models.Where(s =>
                s.ServiceName.Contains(search) ||
                search == null).ToPagedList(pageNumber, pageSize));
        }

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

            ViewBag.OrderForm = new OrderFormVM();

            ViewBag.Services = new ServiceComponents().ListAll().GetRange(0, 6);
            ViewBag.ServicesReverse = new ServiceComponents().ListAllReverse().GetRange(0, 6);
            var services = new ServiceComponents().ListAll();
            SelectList serviceList = new SelectList(services, "ServiceId", "ServiceName");
            ViewBag.Service = serviceList;
            var userLoggedIn = HttpContext.Session.GetObjectFromJson<UserSession>("UserDetails");
            if(userLoggedIn != null)
            {
                var userLoggedInData = _context.Accounts.Where(a => a.AccountId == userLoggedIn.UserId).FirstOrDefault();
                
                    ViewBag.UserLoggedInFirstName = userLoggedInData.FirstName;
                    ViewBag.UserLoggedInLastName = userLoggedInData.LastName;
                    ViewBag.UserLoggedInEmail = userLoggedInData.Email;
                    ViewBag.UserLoggedInAddress = userLoggedInData.Address;
                    ViewBag.UserLoggedInPhone = userLoggedInData.Phone;
            }
             else
            {
                return RedirectToAction("Login", "Accounts");
            }
            
            return View(service);
        }

        /*public ActionResult OrderForm()
        {
            var services = new ServiceComponents().ListAll();
            SelectList serviceList = new SelectList(services, "ServiceId", "ServiceName");
            ViewBag.Service = serviceList;

            OrderFormVM orderFormVM = new OrderFormVM();

            return PartialView("OrderForm", orderFormVM);
        }*/
    }
}
