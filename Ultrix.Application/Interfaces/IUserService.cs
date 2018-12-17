using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<SignInResult> SignInAsync(LoginUserDto loginUserDto);
        Task SignOutAsync(HttpContext httpContext);
        Task<string> GetUserNameByUserIdAsync(int userId);
        Task<int> GetUserIdByUserNameAsync(string userName);
    }
}
