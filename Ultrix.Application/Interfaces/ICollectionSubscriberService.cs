using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionSubscriberService
    {
        Task<bool> SubscribeToCollectionAsync(int userId, int collectionId);
        Task<bool> UnSubscribeFromCollectionAsync(int userId, int collectionId);
        Task<bool> AuthorizeSubscriberToCollectionAsync(int userId, int collectionId);
        Task<bool> DeAuthorizeSubscriberFromCollectionAsync(int userId, int collectionId);
        Task<IEnumerable<CollectionDto>> GetMySubscribedCollectionsAsync(int userId);
    }
}
