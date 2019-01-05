using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IUserService
    {
        Task<SignUpResultDto> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<SignInResultDto> SignInAsync(LoginUserDto loginUserDto);
        Task SignOutAsync();
        Task<string> GetUserNameByUserIdAsync(int userId);
        Task<int> GetUserIdByEmailAsync(string userName);
        Task<IEnumerable<FilteredApplicationUserDto>> GetUsersAsync(int userId);
    }
}
