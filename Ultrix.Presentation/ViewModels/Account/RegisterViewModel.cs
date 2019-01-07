using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Account
{
    public class RegisterViewModel : AntiForgeryTokenViewModelBase
    {
        private string _username;
        private string _email;

        [Required(ErrorMessage = "Please enter a username."),
         MinLength(3, ErrorMessage = "The length must be at least {1} characters."),
         MaxLength(20, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Text)]
        public string Username
        {
            get => _username.Trim();
            set => _username = value;
        }

        [Required(ErrorMessage = "Please enter an email address."),
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

        public RegisterUserDto GetRegisterUserDto()
        {
            return new RegisterUserDto { UserName = Username, Email = Email, Password = Password };
        }
    }
}
