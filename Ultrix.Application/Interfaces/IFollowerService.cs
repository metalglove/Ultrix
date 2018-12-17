using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IFollowerService
    {
        Task<bool> FollowUserAsync(FollowerDto followerDto);
        Task<bool> UnFollowUserAsync(FollowerDto followerDto);
        Task<List<FollowerDto>> GetFollowersByUserIdAsync(int userId);
        Task<List<FollowerDto>> GetFollowingsByUserIdAsync(int userId);
        Task<List<FollowingDto>> GetMutualFollowingsByUserIdAsync(int userId);
    }
}
