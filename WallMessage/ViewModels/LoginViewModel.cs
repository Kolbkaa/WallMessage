using System.ComponentModel.DataAnnotations;

namespace WallMessage.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
