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
    public class CollectionRepository : IRepository<Collection>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<Collection> _collectionValidator;

        public CollectionRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Collection> collectionValidator)
        {
            _applicationDbContext = applicationDbContext;
            _collectionValidator = collectionValidator;
        }

        public async Task<bool> CreateAsync(Collection entity)
        {
            _collectionValidator.Validate(entity);
            if (await _applicationDbContext.Collections.ContainsAsync(entity))
                throw new CollectionAlreadyExistsException();
            _applicationDbContext.Collections.Add(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingCollectionFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> DeleteAsync(Collection entity)
        {
            if (!await _applicationDbContext.Collections.ContainsAsync(entity))
                throw new CollectionNotFoundException();
            _applicationDbContext.Collections.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingCollectionFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> ExistsAsync(Expression<Func<Collection, bool>> predicate)
        {
            return await _applicationDbContext.Collections.AnyAsync(predicate);
        }
        public async Task<IEnumerable<Collection>> FindManyByExpressionAsync(Expression<Func<Collection, bool>> predicate)
        {
            return await _applicationDbContext.Collections.Where(predicate).ToListAsync();
        }
        public async Task<Collection> FindSingleByExpressionAsync(Expression<Func<Collection, bool>> predicate)
        {
            Collection collection = await _applicationDbContext.Collections.SingleAsync(predicate);
            if (collection == default)
                throw new CollectionNotFoundException();
            return collection;
        }
        public async Task<IEnumerable<Collection>> GetAllAsync()
        {
            return await _applicationDbContext.Collections.ToListAsync();
        }
        public Task<bool> UpdateAsync(Collection entity)
        {
            throw new NotImplementedException();
        }
    }
}
