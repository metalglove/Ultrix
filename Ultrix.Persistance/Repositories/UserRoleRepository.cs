using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class UserRoleRepository : RepositoryBase<UserRole>
    {
        /// <inheritdoc />
        public UserRoleRepository(AppDbContext applicationDbContext, IEntityValidator<UserRole> entityValidator) : base(applicationDbContext, entityValidator)
        {
        }
    }
}
