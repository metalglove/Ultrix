using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Ultrix.Application.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<Follower> _followerRepository;

        public UserService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IRepository<ApplicationUser> userRepository,
            IRepository<Follower> followerRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _followerRepository = followerRepository;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            ApplicationUser applicationUser = DtoToEntityConverter.Convert<ApplicationUser, RegisterUserDto>(registerUserDto);
            applicationUser.UserDetail = new UserDetail { ProfilePictureData = "test" };
            applicationUser.Email = "test@test.nl";
            return await _userManager.CreateAsync(applicationUser, registerUserDto.Password);
        }
        public async Task<SignInResult> SignInAsync(LoginUserDto loginUserDto)
        {
            return await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, true, false);
        }
        public async Task SignOutAsync(HttpContext httpContext)
        {
            // TODO: Refactor SignOutAsync to be clean
            await _signInManager.SignOutAsync();
            foreach (string key in httpContext.Request.Cookies.Keys)
            {
                httpContext.Response.Cookies.Append(key, "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            }
            if (httpContext.User.Identity.IsAuthenticated)
            {
                await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                httpContext.User = null;
            }
        }
        public async Task<string> GetUserNameByUserIdAsync(int userId)
        {
            // ApplicationUser applicationUser = await _userManager.FindByIdAsync(); 
            // TODO: UserManager uses string as identifier. Would need to create a new UserManager from scratch to implement with int.
            ApplicationUser applicationUser = await _userRepository.FindSingleByExpressionAsync(user => user.Id.Equals(userId));
            return applicationUser.UserName;
        }
        public async Task<int> GetUserIdByUserNameAsync(string username)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(username); 
            //ApplicationUser applicationUser = await _userRepository.FindSingleByExpressionAsync(user => user.UserName.Equals(username));
            return applicationUser.Id;
        }
        public async Task<IEnumerable<FilteredApplicationUserDto>> GetUsersAsync(int userId)
        {
            IEnumerable<ApplicationUser> users = await _userRepository.GetAllAsync();
            IEnumerable<ApplicationUserDto> usersDtos = users.Where(user => user.Id != userId)
                .Select(EntityToDtoConverter.Convert<ApplicationUserDto, ApplicationUser>);
            IEnumerable<Follower> followings = await _followerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(userId));
            return usersDtos.Select(userDto => new FilteredApplicationUserDto { ApplicationUserDto = userDto, IsFollowed = followings.Any(following => following.UserId.Equals(userDto.Id)) });
        }
    }
}
