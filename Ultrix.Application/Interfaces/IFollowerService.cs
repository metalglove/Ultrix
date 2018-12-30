using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IFollowerService
    {
        Task<ServiceResponseDto> FollowUserAsync(FollowerDto followerDto);
        Task<ServiceResponseDto> UnFollowUserAsync(FollowerDto followerDto);
        Task<IEnumerable<FollowerDto>> GetFollowersByUserIdAsync(int userId);
        Task<IEnumerable<FollowerDto>> GetFollowsByUserIdAsync(int userId);
        Task<IEnumerable<FollowerDto>> GetMutualFollowersByUserIdAsync(int userId);
    }
}
