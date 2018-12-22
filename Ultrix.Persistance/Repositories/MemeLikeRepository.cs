using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class MemeLikeRepository : RepositoryBase<MemeLike>
    {
        public MemeLikeRepository(ApplicationDbContext applicationDbContext, IEntityValidator<MemeLike> memeLikeValidator) : base(applicationDbContext, memeLikeValidator)
        {
        }
    }
}
