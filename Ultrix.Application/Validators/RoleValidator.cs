using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class RoleValidator : IEntityValidator<Role>
    {
        /// <inheritdoc />
        public bool Validate(Role entity)
        {
            // TODO: check what needs to be validated...
            return true;
        }
    }
}
