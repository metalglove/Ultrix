using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CollectionRepository : RepositoryBase<Collection>
    {
        public CollectionRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Collection> collectionValidator) : base(applicationDbContext, collectionValidator)
        {
        }
    }
}
