using System;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class CredentialValidator : IEntityValidator<Credential>
    {
        /// <inheritdoc />
        public bool Validate(Credential entity)
        {
            // TODO: check what needs to be validated...
            return true;
        }
    }
}
