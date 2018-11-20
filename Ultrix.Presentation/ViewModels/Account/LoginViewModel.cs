using System.ComponentModel.DataAnnotations;

namespace Ultrix.Presentation.ViewModels.Account
{
    public class LoginViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please enter a username."), 
         MinLength(3, ErrorMessage = "The length must be at least {1} characters."), 
         MaxLength(20, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Text)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password."), 
         MinLength(5, ErrorMessage = "The length must be less than {1} characters."),
         MaxLength(255, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
