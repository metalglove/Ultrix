using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;
using System.Linq.Expressions;
using Ultrix.Application.Exceptions;

namespace Ultrix.Persistance.Repositories
{
    public class SharedMemeRepository : IRepository<SharedMeme>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<SharedMeme> _sharedMemeValidator;

        public SharedMemeRepository(
            ApplicationDbContext applicationDbContext,
            IEntityValidator<SharedMeme> sharedMemeValidator)
        {
            _applicationDbContext = applicationDbContext;
            _sharedMemeValidator = sharedMemeValidator;
        }

        public async Task<bool> CreateAsync(SharedMeme entity)
        {
            _sharedMemeValidator.Validate(entity);
            if (await _applicationDbContext.SharedMemes.ContainsAsync(entity))
                throw new SharedMemeAlreadyExistsException();
            await _applicationDbContext.SharedMemes.AddAsync(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingSharedMemeFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> DeleteAsync(SharedMeme entity)
        {
            if (!await _applicationDbContext.SharedMemes.ContainsAsync(entity))
                throw new SharedMemeNotFoundException();
            _applicationDbContext.SharedMemes.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingSharedMemeFailedException();
            }
            return saveSuccess;
        }
        public Task<bool> ExistsAsync(Expression<Func<SharedMeme, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<SharedMeme>> FindManyByExpressionAsync(Expression<Func<SharedMeme, bool>> predicate)
        {
            return await _applicationDbContext.SharedMemes.Where(predicate).ToListAsync();
        }
        public Task<SharedMeme> FindSingleByExpressionAsync(Expression<Func<SharedMeme, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<SharedMeme>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateAsync(SharedMeme entity)
        {
            _sharedMemeValidator.Validate(entity);
            SharedMeme oldSharedMeme = await _applicationDbContext.SharedMemes.SingleOrDefaultAsync(oldEntity => oldEntity.Id.Equals(entity.Id));
            if (oldSharedMeme == default)
                throw new SharedMemeNotFoundException();
            _applicationDbContext.Attach(oldSharedMeme);
            oldSharedMeme.MemeId = entity.MemeId;
            oldSharedMeme.ReceiverUserId = entity.ReceiverUserId;
            oldSharedMeme.IsSeen = entity.IsSeen;
            oldSharedMeme.SenderUserId = entity.SenderUserId;
            _applicationDbContext.Update(oldSharedMeme);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException($"Updating SharedMeme with id: {oldSharedMeme.Id} failed.", ex.InnerException);//
            }
            return saveSuccess;
        }
    }
}
