using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class ShoppingCart
{
    public int IdCart { get; set; }

    public string IdUser { get; set; } = null!;

    public DateTime? CartTime { get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual Account IdUserNavigation { get; set; } = null!;
}
