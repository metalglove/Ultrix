using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Services
{
    public class MemeService : IMemeService
    {
        private readonly IMemeFetcherService _memeFetcherService;
        private readonly IMemeRepository _memeRepository;

        public MemeService(ILocalMemeFetcherService localMemeFetcherService, IMemeRepository memeRepository)
        {
            _memeFetcherService = localMemeFetcherService;
            _memeRepository = memeRepository;
        }

        public async Task<bool> DisLikeMemeAsync(MemeLikeDto memeLikeDto)
        {
            MemeLike memeLike = DtoToEntityConverter.Convert<MemeLike, MemeLikeDto>(memeLikeDto);
            return await _memeRepository.DisLikeMemeAsync(memeLike);
        }
        public async Task<MemeDto> GetMemeAsync(string memeId)
        {
            Meme meme = await _memeRepository.GetMemeAsync(memeId);
            return EntityToDtoConverter.Convert<MemeDto, Meme>(meme);
        }
        public async Task<MemeDto> GetRandomMemeAsync()
        {
            MemeDto memeDto = await _memeFetcherService.GetRandomMemeAsync();
            while (await _memeRepository.DoesMemeExistAsync(memeDto.Id))
            {
                memeDto = await _memeFetcherService.GetRandomMemeAsync();
            }
            Meme meme = DtoToEntityConverter.Convert<Meme, MemeDto>(memeDto);
            await _memeRepository.SaveMemeAsync(meme);
            return memeDto;
        }
        public async Task<List<MemeDto>> GetRandomMemesAsync(int count = 4)
        {
            List<MemeDto> memes = new List<MemeDto>();
            for (int i = 0; i < count; i++)
            {
                memes.Add(await GetRandomMemeAsync());
            }
            return memes;
        }
        public async Task<bool> LikeMemeAsync(MemeLikeDto memeLikeDto)
        {
            MemeLike memeLike = DtoToEntityConverter.Convert<MemeLike, MemeLikeDto>(memeLikeDto);
            return await _memeRepository.LikeMemeAsync(memeLike);
        }
        public async Task<bool> UnDisLikeMemeAsync(string memeId, int userId)
        {
            return await _memeRepository.UnDisLikeMemeAsync(memeId, userId);
        }
        public async Task<bool> UnLikeMemeAsync(string memeId, int userId)
        {
            return await _memeRepository.UnLikeMemeAsync(memeId, userId);
        }
    }
}
