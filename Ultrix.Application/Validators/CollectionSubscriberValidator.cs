using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class CollectionSubscriberValidator : IEntityValidator<CollectionSubscriber>
    {
        public bool Validate(CollectionSubscriber entity)
        {
            if (entity == null)
                throw new EntityValidationException("CollectionSubscriber is null");
            if (entity.UserId == 0)
                throw new EntityValidationException("UserId cannot be 0.");
            if (entity.CollectionId == 0)
                throw new EntityValidationException("CollectionId cannot be 0.");
            if (entity.IsAuthorized)
                throw new EntityValidationException("IsAuthorized cannot be true on creation.");
            return true;
        }
    }
}