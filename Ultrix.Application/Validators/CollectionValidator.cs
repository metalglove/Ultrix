﻿using System;
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
            if (entity.UserId.Equals(null) || entity.UserId.Equals(0))
                throw new ArgumentException("CollectionValidator throws: UserId is not set");
            if (!entity.TimestampAdded.Equals(default))
                throw new ArgumentException("CollectionValidator throws: TimestampAdded is set. (is not allowed must be set in database)");
            return true;
        }
    }
}
