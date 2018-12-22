using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Services
{
    public class MemeLikingService : IMemeLikingService
    {
        private readonly IRepository<MemeLike> _memeLikeRepository;

        public MemeLikingService(IRepository<MemeLike> memeLikeRepository)
        {
            _memeLikeRepository = memeLikeRepository;
        }

        public async Task<bool> DislikeMemeAsync(MemeLikeDto memeLikeDto)
        {
            MemeLike actualMemeLike = DtoToEntityConverter.Convert<MemeLike, MemeLikeDto>(memeLikeDto);
            if (await _memeLikeRepository.ExistsAsync(memeLike => memeLike.MemeId.Equals(memeLikeDto.MemeId) && memeLike.UserId.Equals(memeLikeDto.UserId)))
            {
                actualMemeLike = await _memeLikeRepository.FindSingleByExpressionAsync(memeLike => memeLike.MemeId.Equals(memeLikeDto.MemeId) && memeLike.UserId.Equals(memeLikeDto.UserId));
                actualMemeLike.IsLike = false;
                return await _memeLikeRepository.UpdateAsync(actualMemeLike);
            }
            return await _memeLikeRepository.CreateAsync(actualMemeLike);
        }

        public async Task<bool> LikeMemeAsync(MemeLikeDto memeLikeDto)
        {
            MemeLike actualMemeLike = DtoToEntityConverter.Convert<MemeLike, MemeLikeDto>(memeLikeDto);
            if (await _memeLikeRepository.ExistsAsync(memeLike => memeLike.MemeId.Equals(memeLikeDto.MemeId) && memeLike.UserId.Equals(memeLikeDto.UserId)))
            {
                actualMemeLike = await _memeLikeRepository.FindSingleByExpressionAsync(memeLike => memeLike.MemeId.Equals(memeLikeDto.MemeId) && memeLike.UserId.Equals(memeLikeDto.UserId));
                actualMemeLike.IsLike = true;
                return await _memeLikeRepository.UpdateAsync(actualMemeLike);
            }
            return await _memeLikeRepository.CreateAsync(actualMemeLike);
        }

        public async Task<bool> UnDislikeMemeAsync(string memeId, int userId)
        {
            MemeLike actualMemeLike = await _memeLikeRepository.FindSingleByExpressionAsync(memeLike => memeLike.MemeId.Equals(memeId) && memeLike.UserId.Equals(userId));
            return await _memeLikeRepository.DeleteAsync(actualMemeLike);
        }
        public async Task<bool> UnLikeMemeAsync(string memeId, int userId)
        {
            MemeLike actualMemeLike = await _memeLikeRepository.FindSingleByExpressionAsync(memeLike => memeLike.MemeId.Equals(memeId) && memeLike.UserId.Equals(userId));
            return await _memeLikeRepository.DeleteAsync(actualMemeLike);
        }
    }
}
