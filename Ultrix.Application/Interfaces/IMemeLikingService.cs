using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeLikingService
    {
        Task<bool> LikeMemeAsync(MemeLikeDto memeLikeDto);
        Task<bool> UnLikeMemeAsync(string memeId, int userId);
        Task<bool> DislikeMemeAsync(MemeLikeDto memeLikeDto);
        Task<bool> UnDislikeMemeAsync(string memeId, int userId);
    }
}
