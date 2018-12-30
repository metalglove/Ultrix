using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync();
        Task<CollectionDto> GetCollectionByIdAsync(int collectionId);
        Task<ServiceResponseDto> CreateCollectionAsync(CollectionDto collectionDto);
        Task<IEnumerable<CollectionDto>> GetMyCollectionsAsync(int userId);
        Task<ServiceResponseDto> DeleteCollectionAsync(DeleteCollectionDto deleteCollectionDto);
    }
}
