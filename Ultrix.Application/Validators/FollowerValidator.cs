using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class FollowerValidator : IEntityValidator<Follower>
    {
        public bool Validate(Follower entity)
        {
            if (entity == null)
                throw new EntityValidationException("Follower is null");
            if (entity.FollowerUserId.Equals(default))
                throw new EntityValidationException("FollowerUserId is unset.");
            if (entity.UserId.Equals(default))
                throw new EntityValidationException("UserId is unset.");
            if (!entity.TimestampFollowed.Equals(default))
                throw new EntityValidationException("TimestampFollowed is set, only the database is allowed to set Date related properties.");
            return true;
        }
    }
}
