using Ultrix.Domain.Entities.Authentication;
using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.DTOs
{
    public class SignUpResultDto
    {
        public ApplicationUserDto User { get; set; }
        public bool Success { get; set; }
        public SignUpResultError? Error { get; set; }

        public SignUpResultDto(ApplicationUserDto user = null, bool success = false, SignUpResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }
}
