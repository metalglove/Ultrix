using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Validators
{
    public class ApplicationUserValidator : IEntityValidator<ApplicationUser>
    {
        public bool Validate(ApplicationUser entity)
        {
            // Users are created by the UserManager.
            throw new System.NotImplementedException();
        }
    }
}