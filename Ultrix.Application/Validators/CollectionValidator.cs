using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class CollectionValidator: IEntityValidator<Collection>
    {
        public bool Validate(Collection entity)
        {
            if (entity == null)
                throw new EntityValidationException("Collection is null");
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new EntityValidationException("Name IsNullOrWhiteSpace");
            if (entity.UserId.Equals(default))
                throw new EntityValidationException("UserId is unset.");
            if (!entity.TimestampAdded.Equals(default))
                throw new EntityValidationException("TimestampAdded is set, only the database is allowed to set Date related properties");
            return true;
        }
    }
}
