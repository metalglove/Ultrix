using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Account
{
    public class LoginViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please enter a valid email address."),
         DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a password."), 
         MinLength(5, ErrorMessage = "The length must be less than {1} characters."),
         MaxLength(255, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginUserDto GetLoginUserDto()
        {
            return new LoginUserDto { Email = Email, Password = Password };
        }
    }
}
