using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class CredentialTypeValidator : IEntityValidator<CredentialType>
    {
        /// <inheritdoc />
        public bool Validate(CredentialType entity)
        {
            // TODO: check what needs to be validated...
            return true;
        }
    }
}
