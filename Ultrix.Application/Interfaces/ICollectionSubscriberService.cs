using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionSubscriberService
    {
        Task<ServiceResponseDto> SubscribeToCollectionAsync(int userId, int collectionId);
        Task<ServiceResponseDto> UnSubscribeFromCollectionAsync(int userId, int collectionId);
        Task<ServiceResponseDto> AuthorizeSubscriberToCollectionAsync(int userId, int collectionId);
        Task<ServiceResponseDto> DeAuthorizeSubscriberFromCollectionAsync(int userId, int collectionId);
        Task<IEnumerable<CollectionDto>> GetMySubscribedCollectionsAsync(int userId);
    }
}
