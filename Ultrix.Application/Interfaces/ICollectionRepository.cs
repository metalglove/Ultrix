using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionRepository
    {
        Task<bool> DoesCollectionNameExistAsync(string collectionName);
        Task<Collection> GetCollectionAsync(int collectionId);
        Task<List<Collection>> GetAllCollectionsAsync();
        Task<List<Collection>> GetMyCollectionsAsync(int userId);
        Task<List<Collection>> GetMySubscribedCollectionsAsync(int userId);
        Task<bool> CreateCollectionAsync(Collection collection);
        Task<bool> DeleteCollectionAsync(int collectionId);
        Task<bool> AddToCollectionAsync(Meme meme, int collectionId, int userId);
        Task<bool> RemoveFromCollectionAsync(string memeId, int collectionId);
        Task<bool> AuthorizeSubscriberOnCollectionAsync(int userId, int collectionId);
        Task<bool> DeAuthorizeSubscriberOnCollectionAsync(int userId, int collectionId);
        Task<bool> SubscribeToCollectionAsync(int userId, int collectionId);
        Task<bool> UnSubscribeFromCollectionAsync(int userId, int collectionId);
    }
}
