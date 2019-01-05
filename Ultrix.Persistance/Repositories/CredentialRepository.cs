using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CredentialRepository : RepositoryBase<Credential>
    {
        /// <inheritdoc />
        public CredentialRepository(AppDbContext applicationDbContext, IEntityValidator<Credential> entityValidator) : base(applicationDbContext, entityValidator)
        {
        }
    }
}
