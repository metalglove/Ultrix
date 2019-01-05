using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>
    {
        /// <inheritdoc />
        public PermissionRepository(AppDbContext applicationDbContext, IEntityValidator<Permission> entityValidator) : base(applicationDbContext, entityValidator)
        {
        }
    }
}
