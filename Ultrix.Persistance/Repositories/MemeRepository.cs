using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class MemeRepository : RepositoryBase<Meme>
    {
        public MemeRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Meme> memeValidator) : base(applicationDbContext, memeValidator)
        {
        }
    }
}
