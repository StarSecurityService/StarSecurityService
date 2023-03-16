using Microsoft.AspNetCore.Mvc;
using StarSecurityService.Extentions;

namespace StarSecurityService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        [SessionFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}
