using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class MemeValidator : IEntityValidator<Meme>
    {
        public bool Validate(Meme entity)
        {
            if (entity == null)
                throw new EntityValidationException("Meme is null");
            if (string.IsNullOrWhiteSpace(entity.Id))
                throw new EntityValidationException("Id IsNullOrWhiteSpace");
            if (string.IsNullOrWhiteSpace(entity.ImageUrl))
                throw new EntityValidationException("ImageUrl IsNullOrWhiteSpace.");
            if (!ValidationUtilities.IsUrlValid(entity.ImageUrl))
                throw new EntityValidationException("ImageUrl is not a valid url.");
            if (string.IsNullOrWhiteSpace(entity.Title))
                throw new EntityValidationException("Title IsNullOrWhiteSpace.");
            if (string.IsNullOrWhiteSpace(entity.PageUrl))
                throw new EntityValidationException("PageUrl IsNullOrWhiteSpace.");
            if (!ValidationUtilities.IsUrlValid(entity.PageUrl))
                throw new EntityValidationException("PageUrl is not a valid url.");
            if (entity.VideoUrl != null && !ValidationUtilities.IsUrlValid(entity.VideoUrl))
                throw new EntityValidationException("VideoUrl is not a valid url.");
            if (!entity.TimestampAdded.Equals(default))
                throw new EntityValidationException("TimestampAdded is set, only the database is allowed to set Date related properties.");
            return true;
        }
    }
}
