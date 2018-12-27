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
        private readonly IRepository<Follower> _followerRepository;
        private readonly IRepository<ApplicationUser> _applicationUserRepository;

        public FollowerService(IRepository<Follower> followerRepository, IRepository<ApplicationUser> applicationUserRepository)
        {
            _followerRepository = followerRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<FollowResultDto> FollowUserAsync(FollowerDto followerDto)
        {
            FollowResultDto followResultDto = new FollowResultDto();
            Follower follower = DtoToEntityConverter.Convert<Follower, FollowerDto>(followerDto);
            string username = (await _applicationUserRepository.FindSingleByExpressionAsync(user => user.Id.Equals(followerDto.UserId))).UserName;

            if (await _followerRepository.ExistsAsync(follow => follow.UserId.Equals(followerDto.UserId) && follow.FollowerUserId.Equals(followerDto.FollowerUserId)))
            {
                followResultDto.Message = $"You already follow {username}";
                return followResultDto;
            }

            if (await _followerRepository.CreateAsync(follower))
            {
                followResultDto.Success = true;
                followResultDto.Message = $"Succesfully followed {username}";
                return followResultDto;
            }

            followResultDto.Message = "Something happened try again later..";
            return followResultDto;
        }
        public async Task<UnFollowResultDto> UnFollowUserAsync(FollowerDto followerDto)
        {
            UnFollowResultDto unFollowResultDto = new UnFollowResultDto();
            Follower follower = DtoToEntityConverter.Convert<Follower, FollowerDto>(followerDto);
            string username = (await _applicationUserRepository.FindSingleByExpressionAsync(user => user.Id.Equals(followerDto.UserId))).UserName;

            if (!await _followerRepository.ExistsAsync(follow => follow.UserId.Equals(followerDto.UserId) && follow.FollowerUserId.Equals(followerDto.FollowerUserId)))
            {
                unFollowResultDto.Message = $"you do not have {username} followed";
                return unFollowResultDto;
            }
            Follower actualFollower = await _followerRepository.FindSingleByExpressionAsync(follwr => follwr.UserId.Equals(follower.UserId) && follwr.FollowerUserId.Equals(follower.FollowerUserId));
            if (await _followerRepository.DeleteAsync(actualFollower))
            {
                unFollowResultDto.Success = true;
                unFollowResultDto.Message = $"Succesfully unfollowed {username}";
                return unFollowResultDto;
            }

            unFollowResultDto.Message = "Something happened try again later..";
            return unFollowResultDto;
        }
        public async Task<IEnumerable<FollowerDto>> GetFollowersByUserIdAsync(int userId)
        {
            IEnumerable<Follower> followings = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            IEnumerable<Follower> followers = await _followerRepository.FindManyByExpressionAsync(follower => follower.UserId.Equals(userId));
            List<FollowerDto> followerDtos = followers
                .Select(follower => EntityToDtoConverter.Convert<FollowerDto, Follower>(follower)).ToList();
            _ = followerDtos
                .Where(follower => followings.Any(following => following.UserId.Equals(follower.FollowerUserId)))
                .Select(follower => { follower.IsFollowed = true; return follower; }).ToList();
            return followerDtos;
        }
        public async Task<IEnumerable<FollowerDto>> GetFollowsByUserIdAsync(int userId)
        {
            // TODO: check naming followings or follows,. etc
            IEnumerable<Follower> followings = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            List<FollowerDto> followerDtos = followings
                .Select(follower => EntityToDtoConverter.Convert<FollowerDto, Follower>(follower)).ToList();
            return followerDtos.Select(follower => { follower.IsFollowed = true; return follower; });
        }
        public async Task<IEnumerable<FollowerDto>> GetMutualFollowingsByUserIdAsync(int userId)
        {
            IEnumerable<Follower> followings = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            IEnumerable<Follower> followers = await _followerRepository.FindManyByExpressionAsync(follower => follower.UserId.Equals(userId));
            List<FollowerDto> followerDtos = followers
                .Select(follower => EntityToDtoConverter.Convert<FollowerDto, Follower>(follower)).ToList();
            return followerDtos
                .Where(follower => followings.Any(following => following.UserId.Equals(follower.FollowerUserId)))
                .Select(follower => { follower.IsFollowed = true; return follower; });
        }
    }
}
