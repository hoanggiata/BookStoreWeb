using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class Author
{
    public int IdAuthor { get; set; }

    public string? NameAuthor { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual ICollection<News> News { get; } = new List<News>();
}
