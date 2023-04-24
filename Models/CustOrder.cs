using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class CustOrder
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? CustomerId { get; set; }

    public string? Andress { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; } = new List<OrderHistory>();

    public virtual ICollection<OrderLine> OrderLines { get; } = new List<OrderLine>();
}
