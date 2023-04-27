using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class News
{
    public string IdNews { get; set; } = null!;

    public string? NewsTitle { get; set; }

    public string? NewsContent { get; set; }

    public string? SummaryContent { get; set; }

    public string? NewsTag { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Category? NewsTagNavigation { get; set; }
}
