using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.DTOs
{
    public class AddToRoleResultDto
    {
        public bool Success { get; set; }
        public AddToRoleResultError Error { get; set; }
    }
}
