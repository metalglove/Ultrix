using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.DTOs
{
    public class ChangeSecretResultDto
    {
        public bool Success { get; set; }
        public ChangeSecretResultError? Error { get; set; }

        public ChangeSecretResultDto(bool success = false, ChangeSecretResultError? error = null)
        {
            this.Success = success;
            this.Error = error;
        }
    }
}
