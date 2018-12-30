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
        public async Task<ServiceResponseDto> CreateCollectionAsync(CollectionDto collectionDto)
        {
            ServiceResponseDto createCollectionResultDto = new ServiceResponseDto();
            if (await _collectionRepository.ExistsAsync(collection => collection.Name.Equals(collectionDto.Name)))
            {
                createCollectionResultDto.Message = $"A collection with the name {collectionDto.Name} already exists.";
                return createCollectionResultDto;
            }
            Collection actualCollection = DtoToEntityConverter.Convert<Collection, CollectionDto>(collectionDto);
            if (await _collectionRepository.CreateAsync(actualCollection))
            {
                createCollectionResultDto.Success = true;
                createCollectionResultDto.Message = "The collection is created successfully.";
                return createCollectionResultDto;
            }
            createCollectionResultDto.Message = "Something happened try again later..";
            return createCollectionResultDto;
        }
        public async Task<IEnumerable<CollectionDto>> GetMyCollectionsAsync(int userId)
        {
            // after creating a new collection, all collections but the last created collection in the service is a proxy...
            IEnumerable<Collection> collections = await _collectionRepository.FindManyByExpressionAsync(collection => collection.UserId.Equals(userId));
            return collections.Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
        public async Task<ServiceResponseDto> DeleteCollectionAsync(DeleteCollectionDto deleteCollectionDto)
        {
            ServiceResponseDto deleteCollectionResultDto = new ServiceResponseDto();

            if (!await _collectionRepository.ExistsAsync(collection => collection.Id.Equals(deleteCollectionDto.Id)))
            {
                deleteCollectionResultDto.Message = "The collection does not exist.";
                return deleteCollectionResultDto;
            }

            if (!await _collectionRepository.ExistsAsync(collection => collection.Id.Equals(deleteCollectionDto.Id) && collection.UserId.Equals(deleteCollectionDto.UserId)))
            {
                deleteCollectionResultDto.Message = "The collection does not exist or you are not the owner of the collection.";
                return deleteCollectionResultDto;
            }

            Collection actualCollection = await _collectionRepository.FindSingleByExpressionAsync(collection => collection.Id.Equals(deleteCollectionDto.Id) && collection.UserId.Equals(deleteCollectionDto.UserId));

            if (await _collectionRepository.DeleteAsync(actualCollection))
            {
                deleteCollectionResultDto.Success = true;
                deleteCollectionResultDto.Message = "Successfully deleted the collection.";
                return deleteCollectionResultDto;
            }

            deleteCollectionResultDto.Message = "Something happened try again later..";
            return deleteCollectionResultDto;
        }
    }
}
