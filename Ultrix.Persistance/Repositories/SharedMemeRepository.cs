using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Abstractions;

namespace Ultrix.Persistance.Repositories
{
    public class SharedMemeRepository : RepositoryBase<SharedMeme>
    {
        public SharedMemeRepository(ApplicationDbContext applicationDbContext, IEntityValidator<SharedMeme> sharedMemeValidator) : base(applicationDbContext, sharedMemeValidator)
        {
        }
    }
}
