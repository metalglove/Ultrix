using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Services
{
    public class CollectionItemDetailService : ICollectionItemDetailService
    {
        private readonly IRepository<CollectionItemDetail> _collectionItemDetailRepository;
        private readonly IRepository<Collection> _collectionRepository;
        private readonly IRepository<Meme> _memeRepository;

        public CollectionItemDetailService(
            IRepository<CollectionItemDetail> collectionItemDetailRepository, 
            IRepository<Collection> collectionRepository,
            IRepository<Meme> memeRepository)
        {
            _collectionItemDetailRepository = collectionItemDetailRepository;
            _collectionRepository = collectionRepository;
            _memeRepository = memeRepository;
        }

        public async Task<AddMemeToCollectionResultDto> AddMemeToCollectionAsync(AddMemeToCollectionDto addMemeToCollectionDto)
        {
            AddMemeToCollectionResultDto addMemeToCollectionResultDto = new AddMemeToCollectionResultDto();
            if (!await _memeRepository.ExistsAsync(meme => meme.Id.Equals(addMemeToCollectionDto.MemeId)))
            {
                addMemeToCollectionResultDto.Message = "Meme does not exist.";
                return addMemeToCollectionResultDto;
            }

            if (await _collectionItemDetailRepository.ExistsAsync(collectionItemDetail => 
            collectionItemDetail.MemeId.Equals(addMemeToCollectionDto.MemeId) && 
            collectionItemDetail.CollectionId.Equals(addMemeToCollectionDto.CollectionId)))
            {
                addMemeToCollectionResultDto.Message = "Meme already exists in the collection.";
                return addMemeToCollectionResultDto;
            }

            CollectionItemDetail actualCollectionItemDetail = new CollectionItemDetail
            {
                AddedByUserId = addMemeToCollectionDto.UserId,
                CollectionId = addMemeToCollectionDto.CollectionId,
                MemeId = addMemeToCollectionDto.MemeId
            };

            if (await _collectionItemDetailRepository.CreateAsync(actualCollectionItemDetail))
            {
                addMemeToCollectionResultDto.Success = true;
                addMemeToCollectionResultDto.Message = "Succesfully added meme to the collection.";
            }
            else
            {
                addMemeToCollectionResultDto.Message = "Failed to add meme to the collection.";
            }
            return addMemeToCollectionResultDto;
        }

        public async Task<bool> RemoveMemeFromCollectionAsync(RemoveMemeFromCollectionDto removeMemeFromCollectionDto)
        {
            bool isOwnerOfCollection = await _collectionRepository.ExistsAsync(collection => 
            collection.Id.Equals(removeMemeFromCollectionDto.CollectionId) && 
            collection.UserId.Equals(removeMemeFromCollectionDto.UserId));

            CollectionItemDetail actualCollectionItemDetail = await _collectionItemDetailRepository
                .FindSingleByExpressionAsync(collectionItemDetail => collectionItemDetail.Id.Equals(removeMemeFromCollectionDto.CollectionItemDetailId));

            // TODO: check if user is currently subscribed?

            if (!actualCollectionItemDetail.AddedByUserId.Equals(removeMemeFromCollectionDto.UserId) || !isOwnerOfCollection)
                throw new ApplicationUserIsNotAuthorizedException();

            return await _collectionItemDetailRepository.DeleteAsync(actualCollectionItemDetail);
        }
    }
}
