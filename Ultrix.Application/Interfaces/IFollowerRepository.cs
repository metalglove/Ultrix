using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Interfaces
{
    public interface IFollowerRepository
    {
        Task FollowUserAsync(Follower follower);
        Task UnFollowUserAsync(Follower follower);
        Task<IEnumerable<Follower>> GetFollowersAsync(int userId);
    }
}
