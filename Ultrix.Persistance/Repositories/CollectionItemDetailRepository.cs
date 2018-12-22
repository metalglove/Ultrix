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
    public class CollectionItemDetailRepository : IRepository<CollectionItemDetail>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<CollectionItemDetail> _collectionItemDetailValidator;

        public CollectionItemDetailRepository(ApplicationDbContext applicationDbContext, IEntityValidator<CollectionItemDetail> collectionItemDetailValidator)
        {
            _applicationDbContext = applicationDbContext;
            _collectionItemDetailValidator = collectionItemDetailValidator;
        }

        public async Task<bool> CreateAsync(CollectionItemDetail entity)
        {
            _collectionItemDetailValidator.Validate(entity);
            if (await _applicationDbContext.CollectionItemDetails.AnyAsync(collectionItemDetail =>
            collectionItemDetail.CollectionId.Equals(entity.CollectionId) &&
            collectionItemDetail.MemeId.Equals(entity.MemeId)))
                throw new CollectionItemDetailAlreadyExistsException();
            _applicationDbContext.CollectionItemDetails.Add(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingCollectionItemDetailFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> DeleteAsync(CollectionItemDetail entity)
        {
            if (!await _applicationDbContext.CollectionItemDetails.AnyAsync(collectionItemDetail => 
            collectionItemDetail.CollectionId.Equals(entity.CollectionId) &&
            collectionItemDetail.MemeId.Equals(entity.MemeId) &&
            collectionItemDetail.AddedByUserId.Equals(entity.AddedByUserId)))
                throw new CollectionItemDetailNotFoundException();
            _applicationDbContext.CollectionItemDetails.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingCollectionItemDetailFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> ExistsAsync(Expression<Func<CollectionItemDetail, bool>> predicate)
        {
            return await _applicationDbContext.CollectionItemDetails.AnyAsync(predicate);
        }
        public async Task<IEnumerable<CollectionItemDetail>> FindManyByExpressionAsync(Expression<Func<CollectionItemDetail, bool>> predicate)
        {
            return await _applicationDbContext.CollectionItemDetails.Where(predicate).ToListAsync();
        }
        public async Task<CollectionItemDetail> FindSingleByExpressionAsync(Expression<Func<CollectionItemDetail, bool>> predicate)
        {
            CollectionItemDetail collectionItemDetail = await _applicationDbContext.CollectionItemDetails.SingleOrDefaultAsync(predicate);
            if (collectionItemDetail == default)
                throw new CollectionItemDetailNotFoundException();
            return collectionItemDetail;
        }
        public async Task<IEnumerable<CollectionItemDetail>> GetAllAsync()
        {
            return await _applicationDbContext.CollectionItemDetails.ToListAsync();
        }

        public Task<bool> UpdateAsync(CollectionItemDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
