using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class CredentialTypeValidator : IEntityValidator<CredentialType>
    {
        public bool Validate(CredentialType entity)
        {
            if (entity == null)
                throw new EntityValidationException("CredentialType is null");
            if (entity.Id != 0)
                throw new EntityValidationException("Id cannot be set.");
            if (string.IsNullOrWhiteSpace(entity.Code))
                throw new EntityValidationException("Code IsNullOrWhiteSpace.");
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new EntityValidationException("Name IsNullOrWhiteSpace.");
            return true;
        }
    }
}
