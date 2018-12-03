using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
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
            _collectionValidator.Validate(collection);

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
        public async Task<bool> AuthorizeSubscriberOnCollectionAsync(int userId, int collectionId)
        {
            if (!await _applicationDbContext.Users.AnyAsync(user => user.Id.Equals(userId)))
                throw new ApplicationUserNotFoundException();
            if (!await _applicationDbContext.Collections.AnyAsync(collection => collection.Id.Equals(collectionId)))
                throw new CollectionNotFoundException();

            CollectionSubscriber collectionSubscriber =
                await _applicationDbContext.CollectionSubscribers
                    .SingleOrDefaultAsync(collSub =>
                        collSub.CollectionId.Equals(collectionId) &&
                        collSub.UserId.Equals(userId));

            if (collectionSubscriber.Equals(default))
                throw new CollectionSubscriberNotFoundException();

            collectionSubscriber.IsAuthorized = true;
            _applicationDbContext.CollectionSubscribers.Update(collectionSubscriber);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeAuthorizeSubscriberOnCollectionAsync(int userId, int collectionId)
        {
            await DoesApplicationUserExist(userId);
            await DoesCollectionExist(collectionId);

            CollectionSubscriber collectionSubscriber =
                await _applicationDbContext.CollectionSubscribers
                    .SingleOrDefaultAsync(collSub =>
                        collSub.CollectionId.Equals(collectionId) &&
                        collSub.UserId.Equals(userId));

            if (collectionSubscriber.Equals(default))
                throw new CollectionSubscriberNotFoundException();

            collectionSubscriber.IsAuthorized = false;
            _applicationDbContext.CollectionSubscribers.Update(collectionSubscriber);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteCollectionAsync(int userId, int collectionId)
        {
            await DoesApplicationUserExist(userId);
            await DoesCollectionExist(collectionId);

            Collection collection = await _applicationDbContext.Collections.FindAsync(collectionId);
            if (!collection.UserId.Equals(userId))
                throw new ApplicationUserIsNotOwnerOfCollectionException();

            _applicationDbContext.Collections.Remove(collection);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DoesCollectionNameExistAsync(string collectionName)
        {
            return await _applicationDbContext.Collections.AnyAsync(collection => collection.Name.Equals(collectionName));
        }
        public async Task<Collection> GetCollectionAsync(int collectionId)
        {
            await DoesCollectionExist(collectionId);
            return await _applicationDbContext.Collections.SingleAsync(coll => coll.Id.Equals(collectionId));
        }
        public async Task<bool> RemoveFromCollectionAsync(int collectionItemDetailId, int collectionId, int userId)
        {
            await DoesApplicationUserExist(userId);
            await DoesCollectionExist(collectionId);
            await DoesCollectionItemDetailExist(collectionItemDetailId);
            bool isOwnerOfCollection = await _applicationDbContext.Collections.AnyAsync(collection =>
                collection.Id.Equals(collectionId) && collection.UserId.Equals(userId));
            CollectionItemDetail collectionItemDetail =
                await _applicationDbContext.CollectionItemDetails.FindAsync(collectionItemDetailId);
            if (!collectionItemDetail.AddedByUserId.Equals(userId) || !isOwnerOfCollection)
                throw new ApplicationUserIsNotAuthorizedException();
            _applicationDbContext.CollectionItemDetails.Remove(collectionItemDetail);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> SubscribeToCollectionAsync(int userId, int collectionId)
        {
            await DoesApplicationUserExist(userId);
            await DoesCollectionExist(collectionId);
            if (await _applicationDbContext.CollectionSubscribers.AnyAsync(collSubscriber =>
                collSubscriber.CollectionId.Equals(collectionId) && collSubscriber.UserId.Equals(userId)))
                throw new ApplicationUserIsAlreadySubscribedException();
            CollectionSubscriber collectionSubscriber = new CollectionSubscriber
            {
                CollectionId = collectionId,
                UserId = userId
            };
            _applicationDbContext.CollectionSubscribers.Add(collectionSubscriber);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UnSubscribeFromCollectionAsync(int userId, int collectionId)
        {
            await DoesApplicationUserExist(userId);
            await DoesCollectionExist(collectionId);
            if (!await _applicationDbContext.CollectionSubscribers.AnyAsync(collSubscriber =>
                collSubscriber.CollectionId.Equals(collectionId) && collSubscriber.UserId.Equals(userId)))
                throw new ApplicationUserIsNotSubscribedException();
            CollectionSubscriber collectionSubscriber =
                await _applicationDbContext.CollectionSubscribers.SingleAsync(collectSub =>
                    collectSub.CollectionId.Equals(collectionId) && collectSub.UserId.Equals(userId));
            _applicationDbContext.CollectionSubscribers.Remove(collectionSubscriber);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        private async Task DoesCollectionItemDetailExist(int collectionItemDetailId)
        {
            if (!await _applicationDbContext.CollectionItemDetails.AnyAsync(collItemDetail => collItemDetail.Id.Equals(collectionItemDetailId)))
                throw new CollectionItemDetailNotFoundException();
        }
        private async Task DoesCollectionExist(int collectionId)
        {
            if (!await _applicationDbContext.Collections.AnyAsync(coll => coll.Id.Equals(collectionId)))
                throw new CollectionNotFoundException();
        }
        private async Task DoesApplicationUserExist(int userId)
        {
            if (!await _applicationDbContext.Users.AnyAsync(user => user.Id.Equals(userId)))
                throw new ApplicationUserNotFoundException();
        }
        private async Task<bool> CollectionsAnyByNameExist(Collection collection)
        {
            return await _applicationDbContext.Collections.AnyAsync(coll => coll.Name.Equals(collection.Name));
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
    }
}
