using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<bool> CreateAsync(ApplicationUser entity)
        {
            // The user manager handles creating users.
            throw new NotImplementedException();
        }
        public Task<bool> DeleteAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExistsAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<ApplicationUser>> FindManyByExpressionAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        public async Task<ApplicationUser> FindSingleByExpressionAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            ApplicationUser applicationUser = await _applicationDbContext.Users.SingleOrDefaultAsync(predicate);
            if (applicationUser == default)
                throw new ApplicationUserNotFoundException();
            return applicationUser;
        }
        public Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
