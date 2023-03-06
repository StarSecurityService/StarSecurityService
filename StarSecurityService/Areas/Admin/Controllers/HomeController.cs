using Microsoft.AspNetCore.Mvc;
using StarSecurityService.Extentions;

namespace StarSecurityService.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]

        [SessionFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}
