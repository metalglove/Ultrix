using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
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
            return await _userRepository.GetUserNameByUserIdAsync(userId);
        }
        public async Task<int> GetUserIdByUserNameAsync(string username)
        {
            return await _userRepository.GetUserIdByUserNameAsync(username);
        }
    }
}
