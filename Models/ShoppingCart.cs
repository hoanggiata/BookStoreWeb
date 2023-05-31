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
    public ShoppingCart() { }
    public ShoppingCart(string id_user)
    {
        IdUser = id_user;
    }
    public static int AddShopping(string id)
    {
        BookstoreContext db = new BookstoreContext();
        ShoppingCart shoppingCart = new ShoppingCart(id);
        db.ShoppingCarts.Add(shoppingCart);
        db.SaveChanges();
        shoppingCart = db.ShoppingCarts.FirstOrDefault(x => x.IdUser == id && x.CartTime ==null);
        return shoppingCart.IdCart;
    }
}
