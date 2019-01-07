using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IUserService
    {
        Task<SignUpResultDto> SignUpAsync(RegisterUserDto registerUserDto);
        Task<SignInResultDto> SignInAsync(LoginUserDto loginUserDto);
        Task SignOutAsync();
        Task<int> GetUserIdByEmailAsync(string email);
        Task<IEnumerable<FilteredApplicationUserDto>> GetFilteredUsersAsync(int userId);
    }
}
