using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class MemeLikeValidator : IEntityValidator<MemeLike>
    {
        public bool Validate(MemeLike entity)
        {
            if (entity == null)
                throw new EntityValidationException("MemeLike is null");
            if (string.IsNullOrWhiteSpace(entity.MemeId))
                throw new EntityValidationException("MemeId IsNullOrWhiteSpace.");
            if (!entity.TimestampAdded.Equals(default))
                throw new EntityValidationException("TimestampAdded is set, only the database is allowed to set Date related properties.");
            return true;
        }
    }
}
