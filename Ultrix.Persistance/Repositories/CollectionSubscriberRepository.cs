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
    public class CollectionSubscriberRepository : IRepository<CollectionSubscriber>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<CollectionSubscriber> _collectionSubscriberValidator;

        public CollectionSubscriberRepository(ApplicationDbContext applicationDbContext, IEntityValidator<CollectionSubscriber> collectionSubscriberValidator)
        {
            _applicationDbContext = applicationDbContext;
            _collectionSubscriberValidator = collectionSubscriberValidator;
        }

        public async Task<bool> CreateAsync(CollectionSubscriber entity)
        {
            _collectionSubscriberValidator.Validate(entity);
            if (await _applicationDbContext.CollectionSubscribers.ContainsAsync(entity))
                throw new CollectionSubscriberAlreadyExistsException();
            await _applicationDbContext.CollectionSubscribers.AddAsync(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingCollectionSubscriberFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> DeleteAsync(CollectionSubscriber entity)
        {
            if (!await _applicationDbContext.CollectionSubscribers.ContainsAsync(entity))
                throw new CollectionSubscriberNotFoundException();
            _applicationDbContext.CollectionSubscribers.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingCollectionSubscriberFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> ExistsAsync(Expression<Func<CollectionSubscriber, bool>> predicate)
        {
            return await _applicationDbContext.CollectionSubscribers.AnyAsync(predicate);
        }
        public async Task<IEnumerable<CollectionSubscriber>> FindManyByExpressionAsync(Expression<Func<CollectionSubscriber, bool>> predicate)
        {
            return await _applicationDbContext.CollectionSubscribers.Where(predicate).ToListAsync();
        }
        public async Task<CollectionSubscriber> FindSingleByExpressionAsync(Expression<Func<CollectionSubscriber, bool>> predicate)
        {
            CollectionSubscriber collectionSubscriber = await _applicationDbContext.CollectionSubscribers.SingleOrDefaultAsync(predicate);
            if (collectionSubscriber == default)
                throw new CollectionSubscriberNotFoundException();
            return collectionSubscriber;
        }
        public async Task<IEnumerable<CollectionSubscriber>> GetAllAsync()
        {
            return await _applicationDbContext.CollectionSubscribers.ToListAsync();
        }
        public async Task<bool> UpdateAsync(CollectionSubscriber entity)
        {
            CollectionSubscriber oldCollectionSubscriber = await _applicationDbContext.CollectionSubscribers.SingleOrDefaultAsync(oldEntity => oldEntity.Id.Equals(entity.Id));
            if (oldCollectionSubscriber == default)
                throw new CollectionSubscriberNotFoundException();
            _applicationDbContext.Attach(oldCollectionSubscriber);
            oldCollectionSubscriber.CollectionId = entity.CollectionId;
            oldCollectionSubscriber.IsAuthorized = entity.IsAuthorized;
            oldCollectionSubscriber.UserId = entity.UserId;
            _applicationDbContext.Update(oldCollectionSubscriber);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException($"Updating CollectionSubscriber with id: {oldCollectionSubscriber.Id} failed.", ex.InnerException);
            }
            return saveSuccess;
        }
    }
}
