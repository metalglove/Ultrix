using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IFollowerService
    {
        Task<FollowResultDto> FollowUserAsync(FollowerDto followerDto);
        Task<UnFollowResultDto> UnFollowUserAsync(FollowerDto followerDto);
        Task<IEnumerable<FollowerDto>> GetFollowersByUserIdAsync(int userId);
        Task<IEnumerable<FollowerDto>> GetFollowsByUserIdAsync(int userId);
        Task<IEnumerable<FollowerDto>> GetMutualFollowingsByUserIdAsync(int userId);
    }
}
