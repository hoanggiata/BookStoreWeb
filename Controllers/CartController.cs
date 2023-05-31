using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Collections;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStoreWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly BookstoreContext db;
        public CartController(IWebHostEnvironment webHostEnvironment, BookstoreContext db)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.db = db;
        }
        private string GetUserID()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
        public IFileInfo getImages(string cate, string id)
        {
            var provider = new PhysicalFileProvider(webHostEnvironment.WebRootPath);
            var content = provider.GetDirectoryContents(Path.Combine("IMG", "books", cate, id));
            var objFiles = content.OrderBy(m => m.LastModified);
            return objFiles.FirstOrDefault();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var shoppingCart = db.ShoppingCarts.FirstOrDefault(x => x.IdUser == GetUserID() && x.CartTime ==null);

            if (shoppingCart != null)
            {
                List<CartItem> items = CartItem.GetAllCartItem(shoppingCart.IdCart);

                var query = from citem in items
                            join book in db.Books
                            on citem.IdProduct equals book.IdBook
                            into AllCartItem
                            from bookInfo in AllCartItem.DefaultIfEmpty()

                            select new { citem, bookInfo };
                var query2 = from citem in items
                             join b in db.Books on citem.IdProduct equals b.IdBook
                             join t in db.Categories on b.IdCate equals t.IdCategory
                             select new
                             {
                                 IdCart = citem.IdCartItem,
                                 BookName = b.NameBook,
                                 product_Price = citem.Price,
                                 Fixed_Price = b.Newcash,
                                 Image = b.Imageurl,
                                 Cate = t.NameCategory,
                                 Quantity = citem.QuantityItem,
                                 BookId = b.IdBook
                             };
                ViewBag.CartItem = query2;
                ViewBag.List_Item = items;
                return View();
            }
            else
            {
                ViewBag.CartItem = null;
                ViewBag.List_Item = null;
                return View();
            }
        }

        [HttpPost]
        public IActionResult RemoveItem(string id)
        {
            string result = CartItem.RemoveItem(id,GetUserID());
            return Json(result);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(string id,int quantity)
        {
            string result = CartItem.UpdateQuantity(id, quantity);
            return Json(result);
        }

        [HttpPost]
        public IActionResult UpdatePrice(string id,string price,int quantity)
        {
            string result = CartItem.UpdatePrice(id, price,quantity);
            return Json(result);
        }
        [HttpGet]
        public IActionResult GetTotalPrice()
        {
            var id_shoppingCart = db.ShoppingCarts.FirstOrDefault(x => x.IdUser == GetUserID());
            var getAllCartItem = (from item in db.CartItems
                                 where item.IdShoppingCart == id_shoppingCart.IdCart
                                 select item).ToList();

            decimal price =0;
            foreach(var item in getAllCartItem)
            {
                price += Convert.ToDecimal(item.Price);
            }
            return Json(price.ToString());
        }

        [HttpPost]
        public IActionResult CheckOut(string[] list_id)
        {
            string result = CartItem.CheckOut(list_id[0]);
            return Json(result);
        }
    }
}
