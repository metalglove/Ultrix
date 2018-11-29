using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<Collection> _collectionValidator;

        public CollectionRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Collection> collectionValidator)
        {
            _applicationDbContext = applicationDbContext;
            _collectionValidator = collectionValidator;
        }

        public async Task<bool> CreateCollectionAsync(Collection collection)
        {
            if (!_collectionValidator.Validate(collection))
                return false;

            if (await CollectionsAnyByNameExist(collection))
                return false;

            _applicationDbContext.Collections.Add(collection);

            await _applicationDbContext.SaveChangesAsync();
            
            return true;
        }
        public async Task<List<Collection>> GetMyCollectionsAsync(int userId)
        {
            return await _applicationDbContext.Collections
                .Where(collection => collection.UserId.Equals(userId)).ToListAsync();
        }
        public async Task<List<Collection>> GetAllCollectionsAsync()
        {
            return await _applicationDbContext.Collections.ToListAsync();
        }
        public async Task<List<Collection>> GetMySubscribedCollectionsAsync(int userId)
        {
            return await _applicationDbContext.CollectionSubscribers
                .Where(collectionSubscriber => collectionSubscriber.UserId.Equals(userId))
                .Select(collectionSubscriber => collectionSubscriber.Collection).ToListAsync();
        }
        public async Task<bool> AddToCollectionAsync(Meme meme, int collectionId, int userId)
        {
            if (await DoesMemeExistInCollectionAsync(meme, collectionId))
                return false;

            Collection selectedCollection = await _applicationDbContext.Collections
                .SingleAsync(collection => collection.Id.Equals(collectionId));

            CollectionItemDetail collectionItemDetail = new CollectionItemDetail
            {
                AddedByUserId = userId,
                CollectionId = selectedCollection.Id,
                MemeId = meme.Id
            };
            selectedCollection.CollectionItemDetails.Add(collectionItemDetail);

            _applicationDbContext.Collections.Update(selectedCollection);

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
        private async Task<bool> DoesMemeExistInCollectionAsync(Meme meme, int collectionId)
        {
            Collection selectedCollection = await _applicationDbContext.Collections
                .FirstOrDefaultAsync(collection => collection.Id.Equals(collectionId));
            if (selectedCollection.Equals(default))
                return false;
            return selectedCollection.CollectionItemDetails
                .Any(collectionItemDetail => collectionItemDetail.MemeId.Equals(meme.Id));
        }

        public Task<bool> AuthorizeSubscriberOnCollectionAsync(int userId, int collectionId)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CollectionsAnyByNameExist(Collection collection)
        {
            return await _applicationDbContext.Collections.AnyAsync(coll => coll.Name.Equals(collection.Name));
        }

        public Task<bool> DeAuthorizeSubscriberOnCollectionAsync(int userId, int collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCollectionAsync(int collectionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesCollectionNameExistAsync(string collectionName)
        {
            return await _applicationDbContext.Collections.AnyAsync(collection => collection.Name.Equals(collectionName));
        }

        public async Task<Collection> GetCollectionAsync(int collectionId)
        {
            return await _applicationDbContext.Collections.SingleOrDefaultAsync(collection => collection.Id.Equals(collectionId));
        }

        public Task<bool> RemoveFromCollectionAsync(string memeId, int collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToCollectionAsync(int userId, int collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeFromCollectionAsync(int userId, int collectionId)
        {
            throw new NotImplementedException();
        }
    }
}
