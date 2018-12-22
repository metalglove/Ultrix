using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class FollowerRepository : IRepository<Follower>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<Follower> _followerValidator;

        public FollowerRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Follower> entityValidator)
        {
            _applicationDbContext = applicationDbContext;
            _followerValidator = entityValidator;
        }

        public async Task<bool> CreateAsync(Follower entity)
        {
            _followerValidator.Validate(entity);
            if (await _applicationDbContext.Followers.ContainsAsync(entity))
                throw new FollowerAlreadyExistsException();
            _applicationDbContext.Followers.Add(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new FollowingUserFailedException();//
            }
            return saveSuccess;
        }
        public async Task<bool> DeleteAsync(Follower entity)
        {
            if (!await _applicationDbContext.Followers.ContainsAsync(entity))
                throw new FollowerNotFoundException();
            _applicationDbContext.Followers.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new UnFollowingUserFailedException();//
            }
            return saveSuccess;
        }
        public async Task<IEnumerable<Follower>> FindManyByExpressionAsync(Expression<Func<Follower, bool>> predicate)
        {
            return await _applicationDbContext.Followers.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<Follower>> GetAllAsync()
        {
            return await _applicationDbContext.Followers.ToListAsync();
        }
        public async Task<Follower> FindSingleByExpressionAsync(Expression<Func<Follower, bool>> predicate)
        {
            Follower follower = await _applicationDbContext.Followers.SingleOrDefaultAsync(predicate);
            if (follower == default)
                throw new FollowerNotFoundException();
            return follower;
        }
        public Task<bool> UpdateAsync(Follower entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExistsAsync(Expression<Func<Follower, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
