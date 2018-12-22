using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class CollectionItemDetailValidator : IEntityValidator<CollectionItemDetail>
    {
        public bool Validate(CollectionItemDetail entity)
        {
            if (entity == null)
                throw new EntityValidationException("CollectionItemDetail is null");
            if (entity.AddedByUserId == 0)
                throw new EntityValidationException("AddedByUserId cannot be 0.");
            if (entity.CollectionId == 0)
                throw new EntityValidationException("CollectionId cannot be 0.");
            if (string.IsNullOrWhiteSpace(entity.MemeId))
                throw new EntityValidationException("MemeId IsNullOrWhiteSpace.");
            if (!entity.TimestampAdded.Equals(default))
                throw new EntityValidationException("TimestampAdded is set, only the database is allowed to set Date related properties.");
            return true;
        }
    }
}