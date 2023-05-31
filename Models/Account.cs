using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class Account
{
    public string AccountId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public string? AccountDes { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; } = new List<ShoppingCart>();

    public Account(string? username, string? password, string? email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
    public Account() { }

    public static List<Account> GetAllUserCmt(List<Comment> comments)
    {
        BookstoreContext db = new BookstoreContext();
        List<Account> listUserCMT = new List<Account>();
        foreach (Comment item in comments)
        {
            listUserCMT = (from temp in db.Accounts
                           where temp.AccountId == item.IdUser
                           select temp).ToList();
        }
        return listUserCMT;
    }
}
