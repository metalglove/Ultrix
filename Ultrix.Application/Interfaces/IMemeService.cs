using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeService
    {
        Task<MemeDto> GetRandomMemeAsync();
        Task<MemeDto> GetMemeAsync(string memeId);
        Task<IEnumerable<MemeDto>> GetRandomMemesAsync(int count);
        Task<bool> LikeMemeAsync(MemeLikeDto memeLikeDto);
        Task<bool> UnLikeMemeAsync(string memeId, int userId);
        Task<bool> DislikeMemeAsync(MemeLikeDto memeLikeDto);
        Task<bool> UnDislikeMemeAsync(string memeId, int userId);
        Task<SharedMemeResultDto> ShareMemeToFriendAsync(SharedMemeDto sharedMemeDto);
    }
}
