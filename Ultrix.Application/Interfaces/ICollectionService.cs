using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync();
        Task<CollectionDto> GetCollectionByIdAsync(int collectionId);
        Task<CreateCollectionResultDto> CreateCollectionAsync(CollectionDto collectionDto);
        Task<IEnumerable<CollectionDto>> GetMyCollectionsAsync(int userId);
        Task<bool> DeleteCollectionAsync(int userId, int collectionId);
    }
}
