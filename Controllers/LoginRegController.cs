using BookStoreWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using System.Security.Claims;

namespace BookStoreWeb.Controllers
{
    public class LoginRegController : Controller
    {
        BookstoreContext? db;
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username,string password)
        {
            //kiem tra null
            //Kiem tra nguoi dung trong database
            var hashpass = Utility.HashingPassword(password);
            db = new BookstoreContext();
            var acc = db.Accounts.FirstOrDefault(x => x.Username == username && x.Password == hashpass);

            if(acc != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,acc.Username),

                },"login");
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(principal);
                return Redirect("~/Home/Index");
            }
            else
            {
                ViewBag.Error = "<div class='error'>Sai tên tài khoản hoặc mật khẩu</div>";
                //ModelState.AddModelError("login", "Sai ten tai khoan hoac mat khau");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reg(string username,string password,string email)
        {
                Account acc = new Account(username, password, email);
                Utility utility = new Utility();
                utility.CreateUser(acc);
                return RedirectToAction("Index");
        }
    }
}
