using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class OrderHistory
{
    public int HistoryId { get; set; }

    public int? OrderId { get; set; }

    public int? OrderStatus { get; set; }

    public DateTime? StatusDate { get; set; }

    public virtual CustOrder? Order { get; set; }
}
