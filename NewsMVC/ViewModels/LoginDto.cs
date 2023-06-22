using System.ComponentModel.DataAnnotations;

namespace NewsMVC.ViewModels
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
