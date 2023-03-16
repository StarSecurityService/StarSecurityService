using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StarSecurityService.Components;
using StarSecurityService.Data;
using StarSecurityService.Extentions;
using StarSecurityService.Models;
using StarSecurityService.Models.ViewModels;
using System;
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



/*        [HttpGet]
        public ActionResult OrderForm()
        {
            var services = new ServiceComponents().ListAll();
            SelectList serviceList = new SelectList(services, "ServiceId", "ServiceName");
            ViewBag.Service = serviceList;
            return PartialView("_MyPartialView", model);
        }
        [HttpPost]
        public ActionResult OrderForm(OrderFormVM model)
        {
            if (ModelState.IsValid)
            {
                var services = new ServiceComponents().ListAll();
                SelectList serviceList = new SelectList(services, "ServiceId", "ServiceName");
                ViewBag.Service = serviceList;
                var userLoggedInId = HttpContext.Session.GetObjectFromJson<UserSession>("UserDetails").UserId;
                var userLoggedIn = _context.Accounts.Where(a => a.AccountId == userLoggedInId).FirstOrDefault();
                var service = _context.Services.Where(s => s.ServiceId == model.serviceId).First();
                ViewBag.UserLoggedInFirstName = userLoggedIn.FirstName;
                ViewBag.UserLoggedInLastName = userLoggedIn.LastName;
                ViewBag.UserLoggedInEmail = userLoggedIn.Email;
                ViewBag.UserLoggedInAddress = userLoggedIn.Address;
                ViewBag.UserLoggedInPhone = userLoggedIn.Phone;

                var order = new Order();
                order.AccountId = userLoggedInId;
                order.ServiceId = model.serviceId;
                order.Time = model.startDate;
                order.Duration = model.duration;
                order.Amount = model.amount;
                order.Total = service.Price * model.amount;
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            return PartialView("_MyPartialView", model);
        }*/
    }
}
