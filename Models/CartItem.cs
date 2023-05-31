using MessagePack;
using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class CartItem
{
    public int IdCartItem { get; set; }

    public int IdShoppingCart { get; set; }

    public string IdProduct { get; set; } = null!;

    public int QuantityItem { get; set; }

    public string Price { get; set; } = null!;

    public virtual Book IdProductNavigation { get; set; } = null!;

    public virtual ShoppingCart IdShoppingCartNavigation { get; set; } = null!;

    public CartItem() { }
    public CartItem(string idBook, int quantity, int idShoppingCart)
    {
        IdProduct = idBook;
        QuantityItem = quantity;
        IdShoppingCart = idShoppingCart;
    }

    public static List<CartItem> GetAllCartItem(int id_shoppingCart)
    {
        BookstoreContext db = new BookstoreContext();
        var getAllCartItem = (from item in db.CartItems
                              where item.IdShoppingCart == id_shoppingCart
                              select item).ToList();
        return getAllCartItem;
    }
    public static void AddItemToCart(string idBook, int quantity, string idUser,string Price)
    {
        BookstoreContext db = new BookstoreContext();
       var checkShoppingCart = db.ShoppingCarts.FirstOrDefault(X => X.IdUser == idUser && X.CartTime ==null);
      
        if (checkShoppingCart != null)
        {
            CartItem cart = new CartItem(idBook, quantity, checkShoppingCart.IdCart);
            var temp = Convert.ToDecimal(Price) * quantity;
            cart.Price = temp.ToString() ;
            db.Add(cart);
            db.SaveChanges();
        }
        else
        {
            int idShoppingCart = ShoppingCart.AddShopping(idUser);
            CartItem cart = new CartItem(idBook, quantity, idShoppingCart);
            var temp = Convert.ToDecimal(Price) * quantity;
            cart.Price = temp.ToString();
            db.Add(cart);
            db.SaveChanges();
        }
    }
    public static string RemoveItem(string id_Item,string id_user)
    {
        BookstoreContext db = new BookstoreContext();
        var shoppingCart = db.ShoppingCarts.FirstOrDefault(x => x.IdUser == x.IdUser && x.CartTime == null);
        var product = db.CartItems.FirstOrDefault(x => x.IdProduct == id_Item && x.IdShoppingCart == shoppingCart.IdCart);
        if (product != null)
        {
            db.CartItems.Remove(product); db.SaveChanges();
            return "Thanh Cong"; //success remove item
        }
        else
            return "That Bai"; //failed to remove item
    }
    public static string UpdateQuantity(string id_Item, int quantity)
    {
        BookstoreContext db = new BookstoreContext();
        var product = db.CartItems.FirstOrDefault(x => x.IdProduct == id_Item);

        if (product != null)
        {
            product.QuantityItem = quantity;
            product.IdCartItem = product.IdCartItem;
            db.Update(product);
            db.SaveChanges();
            return "Thanh Cong";
        }
        else
            return "That Bai";

    }
    public static string UpdatePrice(string id, string price,int quantity)
    {
        BookstoreContext db = new BookstoreContext();
        var product = db.CartItems.FirstOrDefault(x => x.IdProduct == id);
        if (product != null)
        {
            product.Price = price;
            product.IdCartItem = product.IdCartItem;
            product.QuantityItem = quantity;
            db.Update(product);
            db.SaveChanges();
            return "Thanh Cong";
        }
        else
            return "That Bai";
    }
    public static string CheckOut(string id_cart)
    {
        BookstoreContext db = new BookstoreContext();
        int id_cartItem = Convert.ToInt32(id_cart);
        var cartItem = db.CartItems.FirstOrDefault(x=> x.IdCartItem == id_cartItem);
        var shoppingCart = db.ShoppingCarts.Find(cartItem.IdShoppingCart);
        if (shoppingCart != null && shoppingCart.CartTime ==null)
        {
            shoppingCart.CartTime = DateTime.Today;
            shoppingCart.IdCart = cartItem.IdShoppingCart;
            db.Update(shoppingCart);
            db.SaveChanges();
            return "Thanh Cong";
        }
        else
            return "That Bai";
      
    }
}
