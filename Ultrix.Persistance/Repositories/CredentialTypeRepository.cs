using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CredentialTypeRepository : RepositoryBase<CredentialType>
    {
        /// <inheritdoc />
        public CredentialTypeRepository(AppDbContext applicationDbContext, IEntityValidator<CredentialType> entityValidator) : base(applicationDbContext, entityValidator)
        {
        }
    }
}
