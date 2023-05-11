using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class CartItem
{
    public int IdCartItem { get; set; }

    public int IdShoppingCart { get; set; }

    public string IdProduct { get; set; } = null!;

    public int? QuantityItem { get; set; }

    public virtual Book IdProductNavigation { get; set; } = null!;

    public virtual ShoppingCart IdShoppingCartNavigation { get; set; } = null!;
}
