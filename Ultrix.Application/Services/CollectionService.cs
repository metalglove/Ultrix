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
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMemeRepository _memeRepository;

        public CollectionService(ICollectionRepository collectionRepository, IMemeRepository memeRepository)
        {
            _collectionRepository = collectionRepository;
            _memeRepository = memeRepository;
        }

        public async Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync()
        {
            IEnumerable<Collection> collections = await _collectionRepository.GetAllCollectionsAsync();
            return collections.Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
        public async Task<CollectionDto> GetCollectionByIdAsync(int collectionId)
        {
            Collection collection = await _collectionRepository.GetCollectionAsync(collectionId);
            return EntityToDtoConverter.Convert<CollectionDto, Collection>(collection);
        }
        public async Task<bool> CreateCollectionAsync(CollectionDto collectionDto)
        {
            if (await _collectionRepository.DoesCollectionNameExistAsync(collectionDto.Name))
                return false;
            Collection collection = DtoToEntityConverter.Convert<Collection, CollectionDto>(collectionDto);
            return await _collectionRepository.CreateCollectionAsync(collection);
        }
        public async Task<bool> AddToCollectionAsync(AddMemeToCollectionDto addMemeToCollectionDto)
        {
            if (!await _memeRepository.DoesMemeExistAsync(addMemeToCollectionDto.MemeId))
                return false;

            if (!await _collectionRepository.DoesCollectionExistAsync(addMemeToCollectionDto.CollectionId))
                return false;

            if (!await _collectionRepository.DoesMemeExistInCollectionAsync(addMemeToCollectionDto.MemeId, addMemeToCollectionDto.CollectionId))
                return false;

            Meme meme = await _memeRepository.GetMemeAsync(addMemeToCollectionDto.MemeId);
            return await _collectionRepository.AddToCollectionAsync(meme, addMemeToCollectionDto.CollectionId,
                addMemeToCollectionDto.UserId);
        }
        public async Task<IEnumerable<CollectionDto>> GetMyCollectionsAsync(int userId)
        {
            IEnumerable<Collection> collections = await _collectionRepository.GetMyCollectionsAsync(userId);
            return collections.Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
        public async Task<IEnumerable<CollectionDto>> GetMySubscribedCollectionsAsync(int userId)
        {
            List<Collection> collections = await _collectionRepository.GetMySubscribedCollectionsAsync(userId);
            return collections.Select(EntityToDtoConverter.Convert<CollectionDto, Collection>);
        }
    }
}
