using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class RolePermissionValidator : IEntityValidator<RolePermission>
    {
        public bool Validate(RolePermission entity)
        {
            if (entity == null)
                throw new EntityValidationException("RoleId is null");
            if (entity.RoleId.Equals(default))
                throw new EntityValidationException("RoleId is unset.");
            if (entity.PermissionId.Equals(default))
                throw new EntityValidationException("PermissionId is unset.");
            return true;
        }
    }
}
