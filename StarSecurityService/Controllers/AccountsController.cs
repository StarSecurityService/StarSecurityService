using Microsoft.AspNetCore.Mvc;
using StarSecurityService;
using StarSecurityService.Extentions;
using StarSecurityService.Data;
using StarSecurityService.Models;
using StarSecurityService.Models.ViewModels;

namespace StarSecurityService.Controllers
{
    public class AccountsController : Controller
    {

        private readonly StarSecurityServiceDbContext _context;
        public AccountsController (StarSecurityServiceDbContext context)
        {
            _context = context;
        }

        // Get-Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Post-Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                if (checkEmail(model.Email) > 0)
                {
                    ModelState.AddModelError("", "Email is already exist!");
                }
                else if (checkCardId(model.CardId) > 0)
                {
                    ModelState.AddModelError("", "Identity number is already exist!");
                }
                else
                {
                    var u = new Account();
                    u.FirstName = model.FirstName;
                    u.LastName = model.LastName;
                    u.Email = model.Email;
                    u.Password = BCrypt.Net.BCrypt.HashPassword(model.Password); /*GetMD5(user.Password + GetRandomKey());*/
                    u.Phone = model.Phone;
                    u.Address = model.Address;
                    u.CardId = model.CardId;
                    u.RoleId= 3;

                    _context.Add(u);
                    await _context.SaveChangesAsync();
                    if (_context.Accounts.FirstOrDefault(i => i.Email == u.Email) != null)
                    {
                        TempData["success"] = "Sign-up successfully!";
                        model = new RegisterVM();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Signup Failed!");
                    }
                }
            }
            return View(model);
        }

        private int checkEmail(string? Email)
        {

            var query = from u in _context.Accounts where u.Email == Email select u;

            return query.Count();
        }
        private int checkCardId(string? CardId)
        {

            var query = from u in _context.Accounts where u.CardId == CardId select u;

            return query.Count();
        }

        // Get-Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Post-Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {


                var data = _context.Accounts.Where(s => s.Email.Equals(model.Email)).ToList();
                var dataRoleId = data.FirstOrDefault().RoleId;
                bool checkPass = BCrypt.Net.BCrypt.Verify(model.Password, data.FirstOrDefault().Password);
                if (checkPass)
                {
                    if (dataRoleId == 3)
                    {
                        //add session
                        var User = new UserSession();
                        User.UserFirstName = data.FirstOrDefault().FirstName.ToString();
                        User.UserLastName = data.FirstOrDefault().LastName.ToString();
                        User.UserRoleId = (int)data.FirstOrDefault().RoleId;
                        User.UserEmail = data.FirstOrDefault().Email.ToString();
                        User.UserCardId = data.FirstOrDefault().CardId.ToString();

                        HttpContext.Session.SetObjectAsJson("UserDetails", User);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (dataRoleId == 1 || dataRoleId == 2)
                    {
                        //add session
                        var User = new UserSession();
                        User.UserFirstName = data.FirstOrDefault().FirstName.ToString();
                        User.UserLastName = data.FirstOrDefault().LastName.ToString();
                        User.UserRoleId = (int)data.FirstOrDefault().RoleId;
                        User.UserEmail = data.FirstOrDefault().Email.ToString();
                        User.UserCardId = data.FirstOrDefault().CardId.ToString();

                        HttpContext.Session.SetObjectAsJson("UserDetails", User);
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }

                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login", "Accounts");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
