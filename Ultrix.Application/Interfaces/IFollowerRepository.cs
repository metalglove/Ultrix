using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Interfaces
{
    public interface IFollowerRepository
    {
        Task<bool> FollowUserAsync(Follower follower);
        Task<bool> UnFollowUserAsync(Follower follower);
        Task<List<Follower>> GetFollowersByUserIdAsync(int userId);
        Task<List<Follower>> GetFollowingsByUserIdAsync(int userId);
    }
}
