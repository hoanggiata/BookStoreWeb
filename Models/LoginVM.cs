using MessagePack;
using Microsoft.Build.Framework;

namespace BookStoreWeb.Models
{
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
