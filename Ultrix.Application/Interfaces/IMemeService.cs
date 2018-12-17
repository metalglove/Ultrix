using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeService
    {
        Task<MemeDto> GetRandomMemeAsync();
        Task<MemeDto> GetMemeAsync(string memeId);
        Task<List<MemeDto>> GetRandomMemesAsync(int count);
        Task<bool> LikeMemeAsync(MemeLikeDto memeLikeDto);
        Task<bool> UnLikeMemeAsync(string memeId, int userId);
        Task<bool> DisLikeMemeAsync(MemeLikeDto memeLikeDto);
        Task<bool> UnDisLikeMemeAsync(string memeId, int userId);
    }
}
