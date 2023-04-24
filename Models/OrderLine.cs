using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class OrderLine
{
    public int LineId { get; set; }

    public int? OrderId { get; set; }

    public string? BookId { get; set; }

    public int? Price { get; set; }

    public virtual CustOrder? Order { get; set; }
}
