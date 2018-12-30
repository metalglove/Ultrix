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

        public async Task<ServiceResponseDto> AddMemeToCollectionAsync(AddMemeToCollectionDto addMemeToCollectionDto)
        {
            ServiceResponseDto serviceResultDto = new ServiceResponseDto();
            if (!await _memeRepository.ExistsAsync(meme => meme.Id.Equals(addMemeToCollectionDto.MemeId)))
            {
                serviceResultDto.Message = "Meme does not exist.";
                return serviceResultDto;
            }

            if (await _collectionItemDetailRepository.ExistsAsync(collectionItemDetail => 
            collectionItemDetail.MemeId.Equals(addMemeToCollectionDto.MemeId) && 
            collectionItemDetail.CollectionId.Equals(addMemeToCollectionDto.CollectionId)))
            {
                serviceResultDto.Message = "Meme already exists in the collection.";
                return serviceResultDto;
            }

            CollectionItemDetail actualCollectionItemDetail = new CollectionItemDetail
            {
                AddedByUserId = addMemeToCollectionDto.UserId,
                CollectionId = addMemeToCollectionDto.CollectionId,
                MemeId = addMemeToCollectionDto.MemeId
            };

            if (await _collectionItemDetailRepository.CreateAsync(actualCollectionItemDetail))
            {
                serviceResultDto.Success = true;
                serviceResultDto.Message = "Successfully added meme to the collection.";
            }
            else
            {
                serviceResultDto.Message = "Failed to add meme to the collection.";
            }
            return serviceResultDto;
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
