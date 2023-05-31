using BookStoreWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookstoreContext bookstoreContext;

        public HomeController(ILogger<HomeController> logger,BookstoreContext bookstoreContext)
        {
            _logger = logger;
            this.bookstoreContext = bookstoreContext;
        }

        public IActionResult Index()
        {
            Book book = new Book();
            List<Book> result = book.GetAllBooks();
            return View(result);
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

        public IActionResult News()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}