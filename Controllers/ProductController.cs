using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.Dynamic;
using PagedList;
using System.Security.Claims;

namespace BookStoreWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        private string GetUserID()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

        public IOrderedEnumerable<IFileInfo> getImages(string cate, string id)
        {
            var provider = new PhysicalFileProvider(webHostEnvironment.WebRootPath);
            var content = provider.GetDirectoryContents(Path.Combine("IMG", "books", cate, id));
            var objFiles = content.OrderBy(m => m.LastModified);
            return objFiles;
        }

        [HttpGet]   
        public IActionResult Index(int page, string cate = "TL")
        {
            dynamic myModel = new ExpandoObject();
            // myModel.Books = GetBookByCate(cate);
            Pagination pagination = new Pagination();
            Book books = new Book();
            Category cateModel = new Category();
            ViewBag.totalPage = pagination.getTotalPage(cate);
            ViewBag.OnePageOfProducts = books.GetBookByCate(cate).ToPagedList(page, 8);
            ViewBag.CurrentPage = page;
            ViewBag.RecommendBooks = books.GetBookByCate(cate);
            myModel.Cate = cateModel.GetCate();
            foreach (Category item in myModel.Cate)
            {
                if (item.IdCategory == cate)
                {
                    item.CssClass = "active";
                    ViewBag.GetCate = item.IdCategory;
                }
            }
            ViewBag.ListCate = myModel.Cate;
            return View();
        }
        [HttpGet]
        public IActionResult ProductDetail(string id,string cate)
        {
            TempData["idBook"] = id;
            TempData["Cate"] = cate;
            BookstoreContext db = new BookstoreContext();       
             Book result = db.Books.Find(id);
            //Get image Cover
            //string path = (string)"/IMG/books/" + cate.ToString() + "/" + id.ToString() + "/";
            var objFiles = getImages(cate,id);
            List<string> Images = new List<string>();
            foreach(var item in objFiles.ToList())
            {
                Images.Add(item.Name);
            }
            ViewBag.images = Images; //Done
            var query = from cmt in Comment.listComment(id)
                        join user in db.Accounts
                        on cmt.IdUser equals user.AccountId
                        into AllCommentBook
                        from accName in AllCommentBook.DefaultIfEmpty()

                        select new { cmt, accName };
            int totalRating =0 ;
            foreach(var item in query)
            {
                totalRating += Convert.ToInt32(item.cmt.StarRating);
            }
            if(query.Count()>0)
            {
                ViewBag.TotalRating = totalRating / query.Count();
            }
            else
            {
                ViewBag.TotalRating = 0;
            }
            ViewBag.SumRating = query.Count();
            ViewBag.Query = query;
            ViewBag.UserID = GetUserID();
            return View(result); 
        }
          [HttpPost]
          public IActionResult ProductDetail(string comment_content,int rating)
          {
              Comment.CreateComment(comment_content, GetUserID(), TempData["idBook"].ToString(),rating);
              return RedirectToAction("ProductDetail", new { id=TempData["idBook"].ToString(), cate=TempData["Cate"].ToString()});
          }
        
        [HttpPost]
        public IActionResult PostItem(string idBook,int quantity,string idUser, string PriceItem)
        {
            CartItem.AddItemToCart(idBook, quantity, idUser,PriceItem);
            return Json("Thanh Cong");
        }

        /*   [HttpGet]
           public IActionResult GetComment(string id)
           {
               //query left join get all the comment from that id book and get user name from id

               BookstoreContext db = new BookstoreContext();
               var query = from cmt in Comment.listComment(id)
                           join user in db.Accounts
                           on cmt.IdUser equals user.AccountId
                           into AllCommentBook
                           from accName in AllCommentBook.DefaultIfEmpty()

                           select new { cmt, accName };
               //end query
               return Json(query);
           }
           [HttpPost]
           public IActionResult PostComment(int rating, string comment_content,string IdBook)
           {
               Comment.CreateComment(comment_content, GetUserID(),IdBook, rating);
               return Json("Thanh Cong");
           } */


    }
}
