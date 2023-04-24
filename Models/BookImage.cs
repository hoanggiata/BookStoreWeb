using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace BookStoreWeb.Models
{
    public class BookImage
    {
        public List<string>? Images { get; set; }
        private readonly IWebHostEnvironment? webHostEnvironment;

        public BookImage(IWebHostEnvironment? webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<string> GetImage(string id,string cate)
        {
            // string path = (string)"~/IMG/books".Concat(cate.ToString()+"/").Concat(id.ToString());
            string path = (string)"/IMG/books" + cate.ToString() + id.ToString();
            var provider = new PhysicalFileProvider(webHostEnvironment.WebRootPath);
            var content = provider.GetDirectoryContents(path);
            Images = new List<string>();
            foreach(var item in content.ToList())
            {
                Images.Add(item.PhysicalPath.ToString());
            }
            return Images;
        }
    }
}
