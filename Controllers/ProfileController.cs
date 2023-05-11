using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace BookStoreWeb.Controllers
{
    public class ProfileController : Controller
    {

        private BookstoreContext db;
        

        [HttpGet]
        public IActionResult Index()
        {
            db = new BookstoreContext();
            Account user = db.Accounts.Find(GetUserID());
            ViewBag.countryList = Utility.CountryList();
            return View(user);
        }
        [HttpPost]
        public IActionResult Index(string? username,string email,string? first_name,string? last_name,string? address,string? country,string? account_des)
        {
            db = new BookstoreContext();
            Account user = db.Accounts.Find(GetUserID());
            user.Email = email;
            user.FirstName = first_name; user.LastName = last_name; user.Address = address;user.Country= country;user.AccountDes= account_des;
            db.Accounts.Update(user);
            db.SaveChanges();
            return Redirect("~/Profile/Index");
        }
        private string GetUserID()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
