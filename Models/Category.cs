using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class Category
{
    public string IdCategory { get; set; } = null!;

    public string? NameCategory { get; set; }

    public string? CssClass { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
