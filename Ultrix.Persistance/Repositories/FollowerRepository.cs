using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class FollowerRepository : RepositoryBase<Follower>
    {
        public FollowerRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Follower> entityValidator) : base(applicationDbContext, entityValidator)
        {
        }
    }
}
