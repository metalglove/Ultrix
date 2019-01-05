using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.DTOs
{
    public class RemoveFromRoleResultDto
    {
        public bool Success { get; set; }
        public RemoveFromRoleResultError Error { get; set; }
    }
}
