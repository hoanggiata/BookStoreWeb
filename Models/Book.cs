using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class Book
{
    public string IdBook { get; set; } = null!;

    public string? NameBook { get; set; }

    public string? Newcash { get; set; }

    public string? Author { get; set; }

    public string? Oldcash { get; set; }

    public double? Dealpercent { get; set; }

    public DateTime? PublishDate { get; set; }

    public string? Size { get; set; }

    public string? LoaiBia { get; set; }

    public string? NhaXuatBan { get; set; }

    public string? Company { get; set; }

    public int? IdAuthor { get; set; }

    public string? IdCate { get; set; }

    public string? Imageurl { get; set; }

    public string? Desbook { get; set; }

    public int? StarSum { get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual Author? IdAuthorNavigation { get; set; }

    public virtual Category? IdCateNavigation { get; set; }
    public List<Book> GetBookExById(string id)
    {
        BookstoreContext db = new BookstoreContext();
        var query = db.Books.ToList();
        List<Book> result = new List<Book>();
        foreach (Book item in query)
        {
            if (item.IdBook != id)
                result.Add(item);
        }
        return result;
    }

    public List<Book> GetAllBooks()
    {
        BookstoreContext db = new BookstoreContext();
        var query = db.Books.ToList();
        return query;
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

}
