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

        public async Task<IEnumerable<SharedMemeDto>> GetSharedMemeDtos(int userId, SeenStatus seenStatus)
        {
            IEnumerable<SharedMeme> sharedMemes;
            if (seenStatus.Equals(SeenStatus.Any))
                sharedMemes = await _sharedMemeRepository.FindManyByExpressionAsync(
                sharedMeme => sharedMeme.ReceiverUserId.Equals(userId) &&
                Convert.ToInt32(sharedMeme.IsSeen).Equals((int)seenStatus));
            else
                sharedMemes = await _sharedMemeRepository.FindManyByExpressionAsync(
                sharedMeme => sharedMeme.ReceiverUserId.Equals(userId));
            return sharedMemes.Select(EntityToDtoConverter.Convert<SharedMemeDto, SharedMeme>);
        }

        public async Task<bool> MarkSharedMemeAsSeenAsync(SharedMemeDto sharedMeme)
        {
            SharedMeme sharedMemeEntity = DtoToEntityConverter.Convert<SharedMeme, SharedMemeDto>(sharedMeme);
            sharedMemeEntity.IsSeen = true;
            return await _sharedMemeRepository.UpdateAsync(sharedMemeEntity);
        }

        public async Task<SharedMemeResultDto> ShareMemeToMutualFollowerAsync(SharedMemeDto sharedMemeDto)
        {
            SharedMeme sharedMeme = DtoToEntityConverter.Convert<SharedMeme, SharedMemeDto>(sharedMemeDto);
            string username = (await _userRepository.FindSingleByExpressionAsync(user => user.Id.Equals(sharedMeme.ReceiverUserId))).UserName;

            if (await _sharedMemeRepository.CreateAsync(sharedMeme))
                return new SharedMemeResultDto { ReceiverUsername = username, Success = true };

            return new SharedMemeResultDto { ReceiverUsername = username, Success = false };
        }
    }
}
