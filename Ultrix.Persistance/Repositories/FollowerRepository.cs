using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<Follower> _followerValidator;

        public FollowerRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Follower> followerValidator)
        {
            _applicationDbContext = applicationDbContext;
            _followerValidator = followerValidator;
        }

        public async Task FollowUserAsync(Follower follower)
        {
            _followerValidator.Validate(follower);
            if (await _applicationDbContext.Followers.ContainsAsync(follower))
                throw new FollowerAlreadyExistsException();
            _applicationDbContext.Followers.Add(follower);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UnFollowUserAsync(Follower follower)
        {
            if (!await _applicationDbContext.Followers.ContainsAsync(follower))
                throw new FollowerNotFoundException();
            _applicationDbContext.Followers.Remove(follower);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Follower>> GetFollowersAsync(int userId)
        {
            if (await _applicationDbContext.Followers.AnyAsync(follower => follower.UserId.Equals(userId)))
                return default;
            return await _applicationDbContext.Followers.Where(follower => follower.UserId.Equals(userId)).ToListAsync();
        }
    }
}
