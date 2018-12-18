using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly IFollowerRepository _followerRepository;

        public FollowerService(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }
        public async Task<bool> FollowUserAsync(FollowerDto followerDto)
        {
            Follower follower = DtoToEntityConverter.Convert<Follower, FollowerDto>(followerDto);
            return await _followerRepository.FollowUserAsync(follower);
        }
        public async Task<bool> UnFollowUserAsync(FollowerDto followerDto)
        {
            Follower follower = DtoToEntityConverter.Convert<Follower, FollowerDto>(followerDto);
            return await _followerRepository.FollowUserAsync(follower);
        }
        public async Task<IEnumerable<FollowerDto>> GetFollowersByUserIdAsync(int userId)
        {
            IEnumerable<Follower> followers = await _followerRepository.GetFollowersByUserIdAsync(userId);
            return followers.Select(follower => EntityToDtoConverter.Convert<FollowerDto, Follower>(follower));
        }
        public async Task<IEnumerable<FollowerDto>> GetFollowingsByUserIdAsync(int userId)
        {
            IEnumerable<Follower> followings = await _followerRepository.GetFollowingsByUserIdAsync(userId);
            return followings.Select(follower => EntityToDtoConverter.Convert<FollowerDto, Follower>(follower));
        }
        public async Task<IEnumerable<FollowingDto>> GetMutualFollowingsByUserIdAsync(int userId)
        {
            IEnumerable<Follower> followings = await _followerRepository.GetFollowingsByUserIdAsync(userId);
            IEnumerable<Follower> followers = await _followerRepository.GetFollowersByUserIdAsync(userId);
            return followers.Join(followings, follower => follower.UserId, following => following.FollowerUserId,
                (follower, following) => following).Select(follower => new FollowingDto
                {
                    Id = follower.FollowerUserId,
                    Username = follower.FollowerUser.UserName
                });
        }
    }
}
