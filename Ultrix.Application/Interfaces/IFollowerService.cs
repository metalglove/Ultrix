using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IFollowerService
    {
        Task<bool> FollowUserAsync(FollowerDto followerDto);
        Task<bool> UnFollowUserAsync(FollowerDto followerDto);
        Task<IEnumerable<FollowerDto>> GetFollowersByUserIdAsync(int userId);
        Task<IEnumerable<FollowerDto>> GetFollowingsByUserIdAsync(int userId);
        Task<IEnumerable<FollowingDto>> GetMutualFollowingsByUserIdAsync(int userId);
    }
}
