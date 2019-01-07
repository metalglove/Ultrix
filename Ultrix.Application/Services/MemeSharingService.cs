using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.Services
{
    public class MemeSharingService : IMemeSharingService
    {
        private readonly IRepository<SharedMeme> _sharedMemeRepository;
        private readonly IRepository<ApplicationUser> _userRepository;

        public MemeSharingService(IRepository<SharedMeme> sharedMemeRepository, IRepository<ApplicationUser> userRepository)
        {
            _sharedMemeRepository = sharedMemeRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<SharedMemeDto>> GetSharedMemesAsync(int userId, SeenStatus seenStatus)
        {
            IEnumerable<SharedMeme> sharedMemes;
            if (!seenStatus.Equals(SeenStatus.Any))
                sharedMemes = await _sharedMemeRepository.FindManyByExpressionAsync(
                sharedMeme => sharedMeme.ReceiverUserId.Equals(userId) &&
                Convert.ToInt32(sharedMeme.IsSeen).Equals((int)seenStatus));
            else
                sharedMemes = await _sharedMemeRepository.FindManyByExpressionAsync(
                sharedMeme => sharedMeme.ReceiverUserId.Equals(userId));
            return sharedMemes.Select(EntityToDtoConverter.Convert<SharedMemeDto, SharedMeme>);
        }

        public async Task<ServiceResponseDto> MarkSharedMemeAsSeenAsync(SharedMemeDto sharedMeme)
        {
            ServiceResponseDto sharedMemeMarkAsSeenDto = new ServiceResponseDto
            {
                Message = "Something happened try again later.."
            };

            if (!await _sharedMemeRepository.ExistsAsync(sm => sm.Id.Equals(sharedMeme.Id)))
            {
                sharedMemeMarkAsSeenDto.Message = "Something happened try again later..";
                return sharedMemeMarkAsSeenDto;
            }

            SharedMeme sharedMemeEntity = await _sharedMemeRepository.FindSingleByExpressionAsync(sm => sm.Id.Equals(sharedMeme.Id));
            if (sharedMemeEntity.IsSeen)
            {
                sharedMemeMarkAsSeenDto.Message = "The shared meme is already marked as seen.";
                return sharedMemeMarkAsSeenDto;
            }

            sharedMemeEntity.IsSeen = true;

            if (!await _sharedMemeRepository.UpdateAsync(sharedMemeEntity))
                return sharedMemeMarkAsSeenDto;

            sharedMemeMarkAsSeenDto.Success = true;
            sharedMemeMarkAsSeenDto.Message = "Successfully marked the shared meme as seen.";
            return sharedMemeMarkAsSeenDto;
        }

        public async Task<ServiceResponseDto> ShareMemeToMutualFollowerAsync(SharedMemeDto sharedMemeDto)
        {
            SharedMeme sharedMeme = DtoToEntityConverter.Convert<SharedMeme, SharedMemeDto>(sharedMemeDto);
            string username = (await _userRepository.FindSingleByExpressionAsync(user => user.Id.Equals(sharedMeme.ReceiverUserId))).UserName;

            if (await _sharedMemeRepository.CreateAsync(sharedMeme))
                return new ServiceResponseDto { Message = username, Success = true };

            return new ServiceResponseDto { Message = username, Success = false };
        }
    }
}
