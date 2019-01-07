using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Account
{
    public class LoginViewModel : AntiForgeryTokenViewModelBase
    {
        private string _email;

        [Required(ErrorMessage = "Please enter a valid email address."),
         DataType(DataType.EmailAddress)]
        public string Email
        {
            get => _email.Trim();
            set => _email = value;
        }

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
