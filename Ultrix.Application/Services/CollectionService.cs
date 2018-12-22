using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IRepository<Collection> _collectionRepository;

        public CollectionService(IRepository<Collection> collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync()
        {
            IEnumerable<Collection> collections = await _collectionRepository.GetAllAsync();
            return collections.Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
        public async Task<CollectionDto> GetCollectionByIdAsync(int collectionId)
        {
            Collection actualCollection = await _collectionRepository.FindSingleByExpressionAsync(collection => collection.Id.Equals(collectionId));
            return EntityToDtoConverter.Convert<CollectionDto, Collection>(actualCollection);
        }
        public async Task<bool> CreateCollectionAsync(CollectionDto collectionDto)
        {
            if (await _collectionRepository.ExistsAsync(collection => collection.Name.Equals(collectionDto.Name)))
                return false;
            Collection actualCollection = DtoToEntityConverter.Convert<Collection, CollectionDto>(collectionDto);
            return await _collectionRepository.CreateAsync(actualCollection);
        }
        public async Task<IEnumerable<CollectionDto>> GetMyCollectionsAsync(int userId)
        {
            IEnumerable<Collection> collections = await _collectionRepository.FindManyByExpressionAsync(collection => collection.UserId.Equals(userId));
            return collections.Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
        public async Task<bool> DeleteCollectionAsync(int userId, int collectionId)
        {
            Collection actualCollection = await _collectionRepository.FindSingleByExpressionAsync(collection => collection.Id.Equals(collectionId) && collection.UserId.Equals(userId));
            return await _collectionRepository.DeleteAsync(actualCollection);
        }
    }
}
