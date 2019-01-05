using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Account
{
    public class RegisterViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please enter a username."),
         MinLength(3, ErrorMessage = "The length must be at least {1} characters."),
         MaxLength(20, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Text)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter an email address."), 
         DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a password."),
         MinLength(5, ErrorMessage = "The length must be less than {1} characters."),
         MaxLength(255, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Password)]
        public string Password { get; set; }

        public RegisterUserDto GetRegisterUserDto()
        {
            return new RegisterUserDto { UserName = Username, Email = Email, Password = Password };
        }
    }
}
