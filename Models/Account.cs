using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Models;

public partial class Account
{
    public string AccountId { get; set; } = null!;

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Email { get; set; }

   
    public Account(string? username, string? password, string? email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
    public Account() { }
}
