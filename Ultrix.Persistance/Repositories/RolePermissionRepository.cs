using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class RolePermissionRepository : RepositoryBase<RolePermission>
    {
        /// <inheritdoc />
        public RolePermissionRepository(AppDbContext applicationDbContext, IEntityValidator<RolePermission> entityValidator) : base(applicationDbContext, entityValidator)
        {
        }
    }
}
