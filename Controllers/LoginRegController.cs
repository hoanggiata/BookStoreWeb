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
                    new Claim(ClaimTypes.NameIdentifier,acc.AccountId.ToString()),
                },"Login");
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
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("~/Home/Index");
        }


        [HttpGet]
        public IActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reg(Reg reg)
        {

            //  Account acc = new Account(obj.Username, obj.Password, obj.Email);
            BookstoreContext db = new BookstoreContext();
            bool CheckUserExsist = db.Accounts.Any(x => x.Username == reg.UserName);
            if(CheckUserExsist)
            {
                ViewBag.UserNameMessage = "This User Name is already exsist";
                return View();
            }
            bool CheckEmailExsist = db.Accounts.Any(x => x.Email== reg.Email);
            if(CheckEmailExsist)
            {
                ViewBag.EmailMessage = "This Email is already in use, try another";
                return View();
            }
            Account user = new Account();
            user.Username = reg.UserName;
            user.Password = reg.Password;
            user.Email = reg.Email;
            Utility utility = new Utility();
            utility.CreateUser(user);

                return RedirectToAction("Index");
        }
    }
}
