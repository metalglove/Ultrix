using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class CredentialValidator : IEntityValidator<Credential>
    {
        public bool Validate(Credential entity)
        {
            if (entity == null)
                throw new EntityValidationException("Credential is null");
            if (!entity.Id.Equals(default))
                throw new EntityValidationException("Id cannot be set.");
            if (entity.UserId.Equals(default))
                throw new EntityValidationException("UserId is unset.");
            if (entity.CredentialTypeId.Equals(default))
                throw new EntityValidationException("CredentialTypeId is unset.");
            if (string.IsNullOrWhiteSpace(entity.Identifier))
                throw new EntityValidationException("Identifier IsNullOrWhiteSpace.");
            if (string.IsNullOrWhiteSpace(entity.Secret))
                throw new EntityValidationException("Secret IsNullOrWhiteSpace.");
            if (string.IsNullOrWhiteSpace(entity.Extra))
                throw new EntityValidationException("Extra IsNullOrWhiteSpace.");
            return true;
        }
    }
}
