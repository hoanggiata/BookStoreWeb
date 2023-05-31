using BookStoreWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using WebMatrix.WebData;

namespace BookStoreWeb.Controllers
{
    public class LoginRegController : Controller
    {
        private readonly BookstoreContext db;
        public LoginRegController(BookstoreContext db)
        {
            this.db = db;
        }
        private string GetUserID()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginVM account)
        {
            //kiem tra null
            //Kiem tra nguoi dung trong database
            var hashpass = Utility.HashingPassword(account.Password);
            var acc = db.Accounts.FirstOrDefault(x => x.Username == account.Username && x.Password == hashpass);

            if(acc != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,acc.Username),
                    new Claim(ClaimTypes.NameIdentifier,acc.AccountId.ToString()),
                },"Login");
                 var principal = new ClaimsPrincipal(identity);
                 var login = HttpContext.SignInAsync(principal);
                return Redirect(TempData["preUrl"].ToString());
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
            return Redirect(TempData["preUrl"].ToString());
        }


        [HttpGet]
        public IActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reg(RegVM reg)
        {

            //  Account acc = new Account(obj.Username, obj.Password, obj.Email);
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            TempData["cssDisplay"] = "none";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if(ModelState.IsValid)
            {
                Account getAccount = db.Accounts.First(x => x.Email == email);
                if (getAccount !=null)
                {
                   string newPass = getAccount.Password = Utility.CreateRandomPassword();
                    getAccount.Password = Utility.HashingPassword(getAccount.Password);
                    db.Accounts.Update(getAccount);
                    db.SaveChanges();
                    Utility.SendEmail(getAccount,newPass);
                    ViewBag.EmailMessage = "<div class='success'>Password has been sent to your email address</div>";
                    TempData["cssDisplay"] = "block";
                    await Task.Delay(3000);
                    return View();
                }
                else
                {
                    ViewBag.EmailMessage = "<div class='error'>This email address does not match our records</div>";
                    TempData["cssDisplay"] = "none";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["cssDisplay"] = "none";
                return View();
            }
                
            else
            {
                TempData["cssDisplay"] = "block";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string? Username, string OldPassword, string Password,string ConfirmPassword)
        {
            if(User.Identity.IsAuthenticated)
            {
                TempData["cssDisplay"] = "none";
                Account getAccount = db.Accounts.Find(GetUserID());
                var oldPass = Utility.HashingPassword(OldPassword);
                if(getAccount.Password == oldPass)
                {
                    var hashpass = Utility.HashingPassword(Password);
                    getAccount.Password = hashpass.ToString();
                    db.Update(getAccount);
                    db.SaveChanges();
                    return RedirectToAction("Index", "LoginReg");
                }
            }
            else
            {
                TempData["cssDisplay"] = "block";
                Account getAccount = db.Accounts.First(x => x.Username == Username);
                if (getAccount == null)
                {
                    ViewBag.UserNameMessage = "This username does not exist";
                    return View();
                }
                else
                {
                    var oldPass = Utility.HashingPassword(OldPassword);
                    if (getAccount.Password == oldPass)
                    {
                        if (ConfirmPassword == Password)
                        {
                            var hashpass = Utility.HashingPassword(Password);
                            getAccount.Password = hashpass.ToString();
                            db.Update(getAccount);
                            db.SaveChanges();
                            await Task.Delay(3000);
                            return RedirectToAction("Index", "LoginReg");
                        }
                        else
                        {
                            ViewBag.ConfirmPass = "Password does not match";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.PasswordMessage = "The password does not match with old password";
                        return View();
                    }
                }
            }
            return View();
        }
    }
}
