using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary;
using OnlineBookLibrary.Extentions;
using StarSecurityService.Data;
using StarSecurityService.Models;

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
        public async Task<IActionResult> Register([Bind("FirstName, LastName, RoleId, Email, Password, Phone, Address, CardId")] Account account)
        {
            if (ModelState.IsValid)
            {
                if (checkEmail(account.Email) > 0)
                {
                    ModelState.AddModelError("", "Email is already exist!");
                }
                else if (checkCardId(account.CardId) > 0)
                {
                    ModelState.AddModelError("", "Email is already exist!");
                }
                else
                {
                    var u = new Account();
                    u.FirstName = account.FirstName;
                    u.LastName = account.LastName;
                    u.Email = account.Email;
                    u.Password = BCrypt.Net.BCrypt.HashPassword(account.Password); /*GetMD5(user.Password + GetRandomKey());*/
                    u.Phone = account.Phone;
                    u.Address = account.Address;
                    u.CardId = account.CardId;
                    u.RoleId= 3;

                    _context.Add(u);
                    await _context.SaveChangesAsync();
                    if (_context.Accounts.FirstOrDefault(i => i.Email == u.Email) != null)
                    {
                        ViewBag.Success = "Signup successfully!";
                        account = new Account();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Signup Failed!");
                    }
                }
            }
            return View(account);
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
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var data = _context.Accounts.Where(s => s.Email.Equals(email)).ToList();
                var dataRoleId = data.FirstOrDefault().RoleId;
                bool checkPass = BCrypt.Net.BCrypt.Verify(password, data.FirstOrDefault().Password);
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
                        User.UserId = data.FirstOrDefault().AccountId;

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
