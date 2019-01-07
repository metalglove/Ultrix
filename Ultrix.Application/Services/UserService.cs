using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Ultrix.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<Follower> _followerRepository;

        public UserService(
            IUserManager userManager,
            IRepository<ApplicationUser> userRepository,
            IRepository<Follower> followerRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _followerRepository = followerRepository;
        }

        public async Task<SignUpResultDto> SignUpAsync(RegisterUserDto registerUserDto)
        {
            return await _userManager.SignUpAsync(registerUserDto.UserName, "Email", registerUserDto.Email, registerUserDto.Password);
        }
        public async Task<SignInResultDto> SignInAsync(LoginUserDto loginUserDto)
        {
            ValidateResultDto resultDto = await _userManager.ValidateAsync("Email", loginUserDto.Email, loginUserDto.Password);
            if (resultDto.Success)
                await _userManager.SignInAsync(resultDto.User);
            return new SignInResultDto { Success = resultDto.Success };
        }
        public async Task SignOutAsync()
        {
            await _userManager.SignOutAsync();
        }
        public async Task<int> GetUserIdByEmailAsync(string email)
        {
            return await _userManager.FindUserIdByEmailAsync(email);
        }
        public async Task<IEnumerable<FilteredApplicationUserDto>> GetFilteredUsersAsync(int userId)
        {
            IEnumerable<ApplicationUser> users = await _userRepository.GetAllAsync();
            IEnumerable<ApplicationUserDto> userDTOs = users.Where(user => user.Id != userId).Select(EntityToDtoConverter.Convert<ApplicationUserDto, ApplicationUser>);
            IEnumerable<Follower> followings = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            return userDTOs.Select(userDto => new FilteredApplicationUserDto { ApplicationUserDto = userDto, IsFollowed = followings.Any(following => following.UserId.Equals(userDto.Id)) });
        }
    }
}
