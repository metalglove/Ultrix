using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Services
{
    public class CollectionSubscriberService : ICollectionSubscriberService
    {
        private readonly IRepository<CollectionSubscriber> _collectionSubscriberRepository;

        public CollectionSubscriberService(IRepository<CollectionSubscriber> collectionSubscriberRepository)
        {
            _collectionSubscriberRepository = collectionSubscriberRepository;
        }

        public async Task<bool> AuthorizeSubscriberToCollectionAsync(int userId, int collectionId)
        {
            CollectionSubscriber actualCollectionSubscriber = await _collectionSubscriberRepository
                .FindSingleByExpressionAsync(collectionSubscriber =>
                collectionSubscriber.CollectionId.Equals(collectionId) &&
                collectionSubscriber.UserId.Equals(userId));
            actualCollectionSubscriber.IsAuthorized = true;
            return await _collectionSubscriberRepository.UpdateAsync(actualCollectionSubscriber);
        }
        public async Task<bool> DeAuthorizeSubscriberFromCollectionAsync(int userId, int collectionId)
        {
            CollectionSubscriber actualCollectionSubscriber = await _collectionSubscriberRepository
                .FindSingleByExpressionAsync(collectionSubscriber =>
                collectionSubscriber.CollectionId.Equals(collectionId) &&
                collectionSubscriber.UserId.Equals(userId));
            actualCollectionSubscriber.IsAuthorized = false;
            return await _collectionSubscriberRepository.UpdateAsync(actualCollectionSubscriber);
        }
        public async Task<IEnumerable<CollectionDto>> GetMySubscribedCollectionsAsync(int userId)
        {
            IEnumerable<CollectionSubscriber> collectionSubscribers = await _collectionSubscriberRepository
                 .FindManyByExpressionAsync(collectionSubscriber => collectionSubscriber.UserId.Equals(userId));
            return collectionSubscribers.Select(collectionSubscriber => collectionSubscriber.Collection)
                .Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
        public async Task<bool> SubscribeToCollectionAsync(int userId, int collectionId)
        {
            if (await _collectionSubscriberRepository.ExistsAsync(collSubscriber =>
                collSubscriber.CollectionId.Equals(collectionId) && collSubscriber.UserId.Equals(userId)))
                throw new ApplicationUserIsAlreadySubscribedException();
            CollectionSubscriber collectionSubscriber = new CollectionSubscriber
            {
                CollectionId = collectionId,
                UserId = userId
            };
            return await _collectionSubscriberRepository.CreateAsync(collectionSubscriber);
        }
        public async Task<bool> UnSubscribeFromCollectionAsync(int userId, int collectionId)
        {
            if (!await _collectionSubscriberRepository.ExistsAsync(collSubscriber =>
                collSubscriber.CollectionId.Equals(collectionId) && collSubscriber.UserId.Equals(userId)))
                throw new ApplicationUserIsNotSubscribedException();
            CollectionSubscriber collectionSubscriber = await _collectionSubscriberRepository
                .FindSingleByExpressionAsync(collectSub => collectSub.CollectionId.Equals(collectionId) && collectSub.UserId.Equals(userId));
            return await _collectionSubscriberRepository.DeleteAsync(collectionSubscriber);
        }
    }
}
