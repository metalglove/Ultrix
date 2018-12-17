using System;
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

        public async Task<bool> FollowUserAsync(Follower follower)
        {
            _followerValidator.Validate(follower);
            if (await _applicationDbContext.Followers.ContainsAsync(follower))
                throw new FollowerAlreadyExistsException();
            _applicationDbContext.Followers.Add(follower);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new FollowingUserFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> UnFollowUserAsync(Follower follower)
        {
            if (!await _applicationDbContext.Followers.ContainsAsync(follower))
                throw new FollowerNotFoundException();
            _applicationDbContext.Followers.Remove(follower);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new UnFollowingUserFailedException();
            }
            return saveSuccess;
        }
        public async Task<List<Follower>> GetFollowersByUserIdAsync(int userId)
        {
            return await _applicationDbContext.Followers.Where(follower => follower.UserId.Equals(userId)).ToListAsync();
        }
        public async Task<List<Follower>> GetFollowingsByUserIdAsync(int userId)
        {
            return await _applicationDbContext.Followers.Where(follower => follower.FollowerUserId.Equals(userId)).ToListAsync();
        }
    }
}
