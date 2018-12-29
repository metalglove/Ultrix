using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>
    {
        public CommentRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Comment> commentValidator) : base(applicationDbContext, commentValidator)
        {
        }
    }
}
