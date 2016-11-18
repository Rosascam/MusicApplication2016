using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please comfirm your password.")]

        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
    }
}