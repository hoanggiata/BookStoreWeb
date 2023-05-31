using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Models
{
    public class RegVM
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required] public string Email { get; set; } = null!;
    }
}
