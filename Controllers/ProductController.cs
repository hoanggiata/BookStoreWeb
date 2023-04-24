using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.Dynamic;
using PagedList;

namespace BookStoreWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Book> GetBookByCate(string cate)
        {
            BookstoreContext db = new BookstoreContext();
            var query = db.Books.ToList();
            List<Book> result = new List<Book>();
            foreach (Book item in query)
            {
                if (item.IdCate == cate)
                    result.Add(item);
            }
            return result;
        }
        public List<Category> GetCate()
        {
            BookstoreContext db = new BookstoreContext();
          
            List<Category> categories = db.Categories.ToList();
            return categories;
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
            ViewBag.totalPage = pagination.getTotalPage(cate);
            ViewBag.OnePageOfProducts = GetBookByCate(cate).ToPagedList(page, 8);
            ViewBag.CurrentPage = page;
            ViewBag.RecommendBooks = GetBookByCate(cate);
            myModel.Cate = GetCate();
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
       
        public IActionResult ProductDetail(string id,string cate)
        {
             BookstoreContext db = new BookstoreContext();       
             Book query = db.Books.Find(id);  
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

            //Get other products except This Product

            return View(query); 
        }
        
    }
}
