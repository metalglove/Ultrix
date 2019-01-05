using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser>
    {
        public UserRepository(AppDbContext applicationDbContext, IEntityValidator<ApplicationUser> entityValidator) : base(applicationDbContext, entityValidator)
        {

        }
    }
}
