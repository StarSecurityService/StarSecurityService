using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StarSecurityService.Components;
using StarSecurityService.Data;
using StarSecurityService.Extentions;
using StarSecurityService.Models;
using StarSecurityService.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace StarSecurityService.Controllers
{
    public class OrderController : Controller
    {
        private readonly StarSecurityServiceDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private INotyfService _notyfService;

        public OrderController(StarSecurityServiceDbContext context, IWebHostEnvironment hostEnvironment, INotyfService notifyService)
        {
            _context = context;
            _hostEnvironment=hostEnvironment;
            _notyfService = notifyService;
        }

        /*public ActionResult MyPartialView()
        {
            // Lấy dữ liệu từ model khác
            MyOtherModel model = GetMyOtherModelData();

            // Trả về partial view với dữ liệu của model khác
            return PartialView("_MyPartialView", model);
        }*/


        [HttpPost]
        public ActionResult Create(OrderFormVM model)
        {
            if (ModelState.IsValid)
            {

                var service = _context.Services.Where(s => s.ServiceId == model.serviceId).First();
                var order = new Order();
                order.AccountId = HttpContext.Session.GetObjectFromJson<UserSession>("UserDetails").UserId;
                order.ServiceId = model.serviceId;
                order.Time = model.startDate;
                order.Duration = model.duration;
                order.Amount = model.amount;
                order.Total = service.Price * model.amount;
                _context.Orders.Add(order);
                _context.SaveChanges();
                _notyfService.Success("Order Added Successfully!");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                Console.WriteLine(messages);
                return View();

            }
            
        }
    }
}
