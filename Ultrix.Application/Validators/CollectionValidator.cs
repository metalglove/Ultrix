using System;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class CollectionValidator: IEntityValidator<Collection>
    {
        public bool Validate(Collection entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new ArgumentException("CollectionValidator throws: IsNullOrWhiteSpace");
            if (entity.TimestampAdded.Equals(null) || entity.TimestampAdded.Equals(default))
                throw new ArgumentException("CollectionValidator throws: Timestamp is default or null");
            if (entity.UserId.Equals(null))
                throw new ArgumentException("CollectionValidator throws: UserId is null");
            return true;
        }
    }
}
