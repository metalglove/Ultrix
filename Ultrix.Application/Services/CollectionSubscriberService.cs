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

        public async Task<ServiceResponseDto> AuthorizeSubscriberToCollectionAsync(int userId, int collectionId)
        {
            ServiceResponseDto serviceResponseDto = new ServiceResponseDto();
            CollectionSubscriber actualCollectionSubscriber = await _collectionSubscriberRepository
                .FindSingleByExpressionAsync(collectionSubscriber =>
                collectionSubscriber.CollectionId.Equals(collectionId) &&
                collectionSubscriber.UserId.Equals(userId));
            actualCollectionSubscriber.IsAuthorized = true;
            if(await _collectionSubscriberRepository.UpdateAsync(actualCollectionSubscriber))
            {
                serviceResponseDto.Success = true;
                serviceResponseDto.Message = "Successfully authorized user to collection.";
            }
            else
            {
                serviceResponseDto.Message = "Failed to authorize user to collection.";
            }
            return serviceResponseDto;
        }
        public async Task<ServiceResponseDto> DeAuthorizeSubscriberFromCollectionAsync(int userId, int collectionId)
        {
            ServiceResponseDto serviceResponseDto = new ServiceResponseDto();
            CollectionSubscriber actualCollectionSubscriber = await _collectionSubscriberRepository
                .FindSingleByExpressionAsync(collectionSubscriber =>
                collectionSubscriber.CollectionId.Equals(collectionId) &&
                collectionSubscriber.UserId.Equals(userId));
            actualCollectionSubscriber.IsAuthorized = false;
            if (await _collectionSubscriberRepository.UpdateAsync(actualCollectionSubscriber))
            {
                serviceResponseDto.Success = true;
                serviceResponseDto.Message = "Successfully de-authorized user from collection.";
            }
            else
            {
                serviceResponseDto.Message = "Failed to de-authorize user from collection.";
            }
            return serviceResponseDto;
        }
        public async Task<IEnumerable<CollectionDto>> GetMySubscribedCollectionsAsync(int userId)
        {
            IEnumerable<CollectionSubscriber> collectionSubscribers = await _collectionSubscriberRepository
                 .FindManyByExpressionAsync(collectionSubscriber => collectionSubscriber.UserId.Equals(userId));
            return collectionSubscribers.Select(collectionSubscriber => collectionSubscriber.Collection)
                .Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
        public async Task<ServiceResponseDto> SubscribeToCollectionAsync(int userId, int collectionId)
        {
            ServiceResponseDto serviceResponseDto = new ServiceResponseDto();
            if (await _collectionSubscriberRepository.ExistsAsync(collSubscriber =>
                collSubscriber.CollectionId.Equals(collectionId) && collSubscriber.UserId.Equals(userId)))
            {
                serviceResponseDto.Message = "User is already subscribed to collection.";
                return serviceResponseDto;
            }
            CollectionSubscriber collectionSubscriber = new CollectionSubscriber
            {
                CollectionId = collectionId,
                UserId = userId
            };
            if (await _collectionSubscriberRepository.CreateAsync(collectionSubscriber))
            {
                serviceResponseDto.Success = true;
                serviceResponseDto.Message = "Successfully subscribed to collection.";
            }
            else
            {
                serviceResponseDto.Message = "Failed to subscribe to collection.";
            }
            return serviceResponseDto; 
        }
        public async Task<ServiceResponseDto> UnSubscribeFromCollectionAsync(int userId, int collectionId)
        {
            ServiceResponseDto serviceResponseDto = new ServiceResponseDto();
            if (!await _collectionSubscriberRepository.ExistsAsync(collSubscriber =>
                collSubscriber.CollectionId.Equals(collectionId) && collSubscriber.UserId.Equals(userId)))
            {
                serviceResponseDto.Message = "User is not subscribed to collection.";
                return serviceResponseDto;
            }

            CollectionSubscriber collectionSubscriber = await _collectionSubscriberRepository
                .FindSingleByExpressionAsync(collectSub => collectSub.CollectionId.Equals(collectionId) && collectSub.UserId.Equals(userId));
            if(await _collectionSubscriberRepository.DeleteAsync(collectionSubscriber))
            {
                serviceResponseDto.Success = true;
                serviceResponseDto.Message = "Successfully unsubscribed from collection.";
            }
            else
            {
                serviceResponseDto.Message = "Failed to unsubscribe from collection.";
            }

            return serviceResponseDto;
        }
    }
}
