using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class PermissionValidator : IEntityValidator<Permission>
    {
        /// <inheritdoc />
        public bool Validate(Permission entity)
        {
            // TODO: check what needs to be validated...
            return true;
        }
    }
}
