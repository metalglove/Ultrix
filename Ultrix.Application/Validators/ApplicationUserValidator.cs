using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class ApplicationUserValidator : IEntityValidator<ApplicationUser>
    {
        public bool Validate(ApplicationUser entity)
        {
            if (entity == null)
                throw new EntityValidationException("CollectionItemDetail is null");
            if (string.IsNullOrWhiteSpace(entity.UserName))
                throw new EntityValidationException("UserName IsNullOrWhiteSpace.");
            if (!entity.TimestampCreated.Equals(default))
                throw new EntityValidationException("TimestampCreated is set, only the database is allowed to set Date related properties.");
            return true;
        }
    }
}