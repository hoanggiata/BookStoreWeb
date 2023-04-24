using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Email { get; set; }

    public int? Phone { get; set; }

    public string? Andress { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; } = new List<CustOrder>();
}
