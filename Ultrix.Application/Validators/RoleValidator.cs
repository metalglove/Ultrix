﻿using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Validators
{
    public class RoleValidator : IEntityValidator<Role>
    {
        public bool Validate(Role entity)
        {
            if (entity == null)
                throw new EntityValidationException("Role is null");
            if (!entity.Id.Equals(default))
                throw new EntityValidationException("Id cannot be set.");
            if (string.IsNullOrWhiteSpace(entity.Code))
                throw new EntityValidationException("Code IsNullOrWhiteSpace.");
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new EntityValidationException("Name IsNullOrWhiteSpace.");
            return true;
        }
    }
}
