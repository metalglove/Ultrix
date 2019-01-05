using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class RolePermissionValidator : IEntityValidator<RolePermission>
    {
        /// <inheritdoc />
        public bool Validate(RolePermission entity)
        {
            // TODO: check what needs to be validated...
            return true;
        }
    }
}
