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

        [HttpGet]
      /*  public IActionResult Index(int? page,string cate = "TL")
        {
            dynamic myModel = new ExpandoObject();
            // myModel.Books = GetBookByCate(cate);
            List<Book> books = GetBookByCate(cate).ToList();
            var pageNumbers = page ?? 1;
            ViewBag.OnePageOfProducts = (IPagedList) books.ToPagedList(pageNumbers, 6);
            myModel.Cate = GetCate();
            foreach(Category item in myModel.Cate)
            {
                if (item.IdCategory == cate)
                {
                    item.CssClass = "active";
                    ViewBag.GetCate = item.IdCategory;
                }
            }
            ViewBag.ListCate = myModel.Cate;
            return View();
        } */
     
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
            var provider = new PhysicalFileProvider(webHostEnvironment.WebRootPath);
            var content = provider.GetDirectoryContents(Path.Combine("IMG","books",cate,id));
            var objFiles = content.OrderBy(m => m.LastModified);
            List<string> Images = new List<string>();
            foreach(var item in objFiles.ToList())
            {
                Images.Add(item.Name);
            }
            ViewBag.images = Images; //Done

            //Get all comment in this book page
           // List<Comment> comments = Utility.listComment(id);
           // List<Account> accounts = Account.GetAllUserCmt(comments);

            //query left join get all the comment from that id book and get user name from id
            var query = from cmt in Comment.listComment(id)
                        join user in db.Accounts
                        on cmt.IdUser equals user.AccountId
                        into AllCommentBook
                        from accName in AllCommentBook.DefaultIfEmpty()

                        select new { cmt, accName };
            ViewBag.Query = query;
            //end query

            //Get User ID
            //Account user = db.Accounts.Find(GetUserID());

            return View(result); 
        }
        [HttpPost]
        public IActionResult ProductDetail(string comment_content)
        {
            Comment.CreateComment(comment_content, GetUserID(), TempData["idBook"].ToString());
            return RedirectToAction("ProductDetail", new { id=TempData["idBook"].ToString(), cate=TempData["Cate"].ToString()});
        }
    }
}
