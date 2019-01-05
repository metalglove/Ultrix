using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class UserRoleValidator : IEntityValidator<UserRole>
    {
        /// <inheritdoc />
        public bool Validate(UserRole entity)
        {
            // TODO: check what needs to be validated...
            return true;
        }
    }
}
