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
        private readonly IRepository<Meme> _memeRepository;

        public MemeService(IMemeFetcherService memeFetcherService, IRepository<Meme> memeRepository)
        {
            _memeFetcherService = memeFetcherService;
            _memeRepository = memeRepository;
        }

        public async Task<MemeDto> GetMemeAsync(string memeId)
        {
            Meme actualMeme = await _memeRepository.FindSingleByExpressionAsync(meme => meme.Id.Equals(memeId));
            return EntityToDtoConverter.Convert<MemeDto, Meme>(actualMeme);
        }
        public async Task<MemeDto> GetRandomMemeAsync()
        {
            MemeDto memeDto = await _memeFetcherService.GetRandomMemeAsync();
            while (await _memeRepository.ExistsAsync(meme => meme.Id.Equals(memeDto.Id)))
            {
                memeDto = await _memeFetcherService.GetRandomMemeAsync();
            }
            Meme actualMeme = DtoToEntityConverter.Convert<Meme, MemeDto>(memeDto);
            await _memeRepository.CreateAsync(actualMeme);
            return memeDto;
        }
    }
}
