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

        public async Task<ServiceResponseDto> FollowUserAsync(FollowerDto followerDto)
        {
            ServiceResponseDto followResultDto = new ServiceResponseDto();
            Follower follower = DtoToEntityConverter.Convert<Follower, FollowerDto>(followerDto);
            if (!await _applicationUserRepository.ExistsAsync(user => user.Id.Equals(followerDto.UserId)))
            {
                followResultDto.Message = "The user does not exist.";
                return followResultDto;
            }

            if (!await _applicationUserRepository.ExistsAsync(user => user.Id.Equals(followerDto.FollowerUserId)))
            {
                followResultDto.Message = "The follower user does not exist.";
                return followResultDto;
            }

            string username = (await _applicationUserRepository.FindSingleByExpressionAsync(user => user.Id.Equals(followerDto.UserId))).UserName;

            if (await _followerRepository.ExistsAsync(follow => follow.UserId.Equals(followerDto.UserId) && follow.FollowerUserId.Equals(followerDto.FollowerUserId)))
            {
                followResultDto.Message = $"You already follow {username}";
                return followResultDto;
            }

            if (await _followerRepository.CreateAsync(follower))
            {
                followResultDto.Success = true;
                followResultDto.Message = $"Successfully followed {username}";
                return followResultDto;
            }

            followResultDto.Message = "Something happened try again later..";
            return followResultDto;
        }
        public async Task<ServiceResponseDto> UnFollowUserAsync(FollowerDto followerDto)
        {
            ServiceResponseDto unFollowResultDto = new ServiceResponseDto();
            Follower follower = DtoToEntityConverter.Convert<Follower, FollowerDto>(followerDto);
            if (!await _applicationUserRepository.ExistsAsync(user => user.Id.Equals(followerDto.UserId)))
            {
                unFollowResultDto.Message = "The user does not exist.";
                return unFollowResultDto;
            }

            if (!await _applicationUserRepository.ExistsAsync(user => user.Id.Equals(followerDto.FollowerUserId)))
            {
                unFollowResultDto.Message = "The follower user does not exist.";
                return unFollowResultDto;
            }

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
                unFollowResultDto.Message = $"Successfully unfollowed {username}";
                return unFollowResultDto;
            }

            unFollowResultDto.Message = "Something happened try again later..";
            return unFollowResultDto;
        }
        public async Task<IEnumerable<FollowerDto>> GetFollowersByUserIdAsync(int userId)
        {
            IEnumerable<Follower> followings = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            IEnumerable<Follower> followers = await _followerRepository.FindManyByExpressionAsync(follower => follower.UserId.Equals(userId));
            List<FollowerDto> followerDTOs = followers
                .Select(EntityToDtoConverter.Convert<FollowerDto, Follower>).ToList();
            _ = followerDTOs
                .Where(follower => followings.Any(following => following.UserId.Equals(follower.FollowerUserId)))
                .Select(follower => { follower.IsFollowed = true; return follower; }).ToList();
            return followerDTOs;
        }
        public async Task<IEnumerable<FollowerDto>> GetFollowsByUserIdAsync(int userId)
        {
            IEnumerable<Follower> follows = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            List<FollowerDto> followerDTOs = follows
                .Select(EntityToDtoConverter.Convert<FollowerDto, Follower>).ToList();
            return followerDTOs.Select(follower => { follower.IsFollowed = true; return follower; });
        }
        public async Task<IEnumerable<FollowerDto>> GetMutualFollowersByUserIdAsync(int userId)
        {
            IEnumerable<Follower> followings = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            IEnumerable<Follower> followers = await _followerRepository.FindManyByExpressionAsync(follower => follower.UserId.Equals(userId));
            List<FollowerDto> followerDTOs = followers
                .Select(EntityToDtoConverter.Convert<FollowerDto, Follower>).ToList();
            return followerDTOs
                .Where(follower => followings.Any(following => following.UserId.Equals(follower.FollowerUserId)))
                .Select(follower => { follower.IsFollowed = true; return follower; });
        }
    }
}
