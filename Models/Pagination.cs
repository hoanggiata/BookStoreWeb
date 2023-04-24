namespace BookStoreWeb.Models
{
    public class Pagination
    {
        public Pagination() { }
        public double getTotalPage(string cate)
        {
            BookstoreContext db = new BookstoreContext();
            var allBooks = db.Books.ToList();
            List<Book> books = new List<Book>();
            foreach (Book item in allBooks)
            {
                if (item.IdCate == cate)
                {
                    books.Add(item);
                }
            }
            double totalPages = Math.Ceiling((double)books.Count() / 8);
            return (totalPages);
        }
    }
}
