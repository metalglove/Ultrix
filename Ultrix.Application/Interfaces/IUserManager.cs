using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Interfaces
{
    public interface IUserManager
    {
        Task<AddToRoleResultDto> AddToRoleAsync(ApplicationUser user, Role role);
        Task<AddToRoleResultDto> AddToRoleAsync(ApplicationUser user, string roleCode);
        Task<ChangeSecretResultDto> ChangeSecretAsync(string credentialTypeCode, string identifier, string secret);
        Task<int> FindUserIdByEmailAsync(string email);
        Task<string> FindUserNameByIdAsync(int userId);
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<int> GetCurrentUserIdAsync();
        Task<RemoveFromRoleResultDto> RemoveFromRoleAsync(ApplicationUser user, Role role);
        Task<RemoveFromRoleResultDto> RemoveFromRoleAsync(ApplicationUser user, string roleCode);
        Task SignInAsync(ApplicationUser user, bool isPersistent = false);
        Task SignOutAsync();
        Task<SignUpResultDto> SignUpAsync(string name, string credentialTypeCode, string identifier);
        Task<SignUpResultDto> SignUpAsync(string name, string credentialTypeCode, string identifier, string secret);
        Task<ValidateResultDto> ValidateAsync(string credentialTypeCode, string identifier);
        Task<ValidateResultDto> ValidateAsync(string credentialTypeCode, string identifier, string secret);
    }
}