using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(AppDbContext applicationDbContext, IEntityValidator<Role> roleValidator) : base(applicationDbContext, roleValidator)
        {
        }
    }
}
