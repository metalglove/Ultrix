using Ultrix.Application.Interfaces;
using Ultrix.Application.Validators.Exceptions;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class SharedMemeValidator : IEntityValidator<SharedMeme>
    {
        public bool Validate(SharedMeme entity)
        {
            if (entity == null)
                throw new EntityValidationException("SharedMeme is null");
            if (entity.IsSeen.Equals(true))
                throw new EntityValidationException("IsSeen is set, a SharedMeme cannot be seen before it is shared...");
            if (string.IsNullOrWhiteSpace(entity.MemeId))
                throw new EntityValidationException("MemeId IsNullOrWhiteSpace.");
            if (entity.ReceiverUserId.Equals(default))
                throw new EntityValidationException("ReceiverUserId is unset.");
            if (entity.SenderUserId.Equals(default))
                throw new EntityValidationException("SenderUserId is unset.");
            if (!entity.TimestampShared.Equals(default))
                throw new EntityValidationException("TimestampShared is set, only the database is allowed to set Date related properties.");
            return true;
        }
    }
}
