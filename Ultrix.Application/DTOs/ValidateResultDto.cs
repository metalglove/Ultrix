using Ultrix.Domain.Entities;
using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.DTOs
{
    public class ValidateResultDto
    {
        public ApplicationUser User { get; set; }
        public bool Success { get; set; }
        public ValidateResultError? Error { get; set; }

        public ValidateResultDto(ApplicationUser user = null, bool success = false, ValidateResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }
}
