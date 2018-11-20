using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionRepository
    {
        Task<bool> DoesCollectionNameExistAsync(string collectionName);
        Task<Collection> GetCollectionAsync(string collectionId);
        Task<List<Collection>> GetMyCollectionsAsync(string userName);
        Task<bool> CreateCollectionAsync(Collection collection);
        Task<bool> DeleteCollectionAsync(string collectionId);
        Task<bool> AddToCollectionAsync(Meme meme, string collectionId);
        Task<bool> RemoveFromCollectionAsync(string memeId, string collectionId);
        Task<bool> AuthorizeSubscriberOnCollectionAsync(string userId, string collectionId);
        Task<bool> DeAuthorizeSubscriberOnCollectionAsync(string userId, string collectionId);
        Task<bool> SubscribeToCollectionAsync(string userId, string collectionId);
        Task<bool> UnSubscribeFromCollectionAsync(string userId, string collectionId);
    }
}
