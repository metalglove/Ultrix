using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class CommentValidator : IEntityValidator<Comment>
    {
        public bool Validate(Comment entity)
        {
            if (entity == null)
                throw new EntityValidationException("Comment is null");
            if (string.IsNullOrWhiteSpace(entity.MemeId))
                throw new EntityValidationException("MemeId IsNullOrWhiteSpace.");
            if (string.IsNullOrWhiteSpace(entity.Text))
                throw new EntityValidationException("Text IsNullOrWhiteSpace.");
            if (entity.UserId.Equals(default))
                throw new EntityValidationException("UserId is unset.");
            if (entity.Text.Length < 10)
                throw new EntityValidationException("Text length is lower than 10.");
            if (!entity.TimestampAdded.Equals(default))
                throw new EntityValidationException("TimestampAdded is set, only the database is allowed to set Date related properties.");
            return true;
        }
    }
}
