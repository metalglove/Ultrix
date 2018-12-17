using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionService
    {
        Task<List<CollectionDto>> GetAllCollectionsAsync();
        Task<CollectionDto> GetCollectionByIdAsync(int collectionId);
        Task<bool> CreateCollectionAsync(CollectionDto collectionDto);
        Task<bool> AddToCollectionAsync(AddMemeToCollectionDto addMemeToCollectionDto);
        Task<List<CollectionDto>> GetMyCollectionsAsync(int userId);
        Task<List<CollectionDto>> GetMySubscribedCollectionsAsync(int userId);
    }
}
