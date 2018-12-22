using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class MemeLikeRepository : IRepository<MemeLike>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<MemeLike> _memeLikeValidator;

        public MemeLikeRepository(ApplicationDbContext applicationDbContext, IEntityValidator<MemeLike> memeLikeValidator)
        {
            _applicationDbContext = applicationDbContext;
            _memeLikeValidator = memeLikeValidator;
        }

        public async Task<bool> CreateAsync(MemeLike entity)
        {
            _memeLikeValidator.Validate(entity);
            if (await _applicationDbContext.MemeLikes.ContainsAsync(entity))
                throw new MemeLikeAlreadyExistsException();
            await _applicationDbContext.MemeLikes.AddAsync(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingMemeLikeFailedException();
            }
            return saveSuccess;
        }

        public async Task<bool> DeleteAsync(MemeLike entity)
        {
            if (!await _applicationDbContext.MemeLikes.ContainsAsync(entity))
                throw new MemeLikeNotFoundException();
            _applicationDbContext.MemeLikes.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingMemeLikeFailedException();//
            }
            return saveSuccess;
        }

        public async Task<bool> ExistsAsync(Expression<Func<MemeLike, bool>> predicate)
        {
            return await _applicationDbContext.MemeLikes.AnyAsync(predicate);
        }

        public Task<IEnumerable<MemeLike>> FindManyByExpressionAsync(Expression<Func<MemeLike, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<MemeLike> FindSingleByExpressionAsync(Expression<Func<MemeLike, bool>> predicate)
        {
            MemeLike memeLike = await _applicationDbContext.MemeLikes.SingleOrDefaultAsync(predicate);
            if (memeLike == default)
                throw new MemeLikeNotFoundException();
            return memeLike;
        }

        public Task<IEnumerable<MemeLike>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(MemeLike entity)
        {
            MemeLike oldMemeLike = await _applicationDbContext.MemeLikes.SingleOrDefaultAsync(oldEntity => oldEntity.Id.Equals(entity.Id));
            if (oldMemeLike == default)
                throw new MemeLikeNotFoundException();
            _applicationDbContext.Attach(oldMemeLike);
            oldMemeLike.IsLike = entity.IsLike;
            oldMemeLike.MemeId = entity.MemeId;
            oldMemeLike.UserId = entity.UserId;
            _applicationDbContext.Update(oldMemeLike);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException($"Updating MemeLike with id: {oldMemeLike.Id} failed.", ex.InnerException);//
            }
            return saveSuccess;
        }
    }
}
