using Microsoft.AspNetCore.Mvc;
using StarSecurityService.Components;
using StarSecurityService.Models;
using System.Diagnostics;
using StarSecurityService.Extentions;

namespace StarSecurityService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [SessionAdminFilter]
        public IActionResult Index()
        {
            ViewBag.Services = new ServiceComponents().ListAll().GetRange(0, 6);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}