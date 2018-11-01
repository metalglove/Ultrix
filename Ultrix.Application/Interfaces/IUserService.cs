using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password);
        Task<SignInResult> SignInAsync(string userName, string password);
        Task SignOutAsync(HttpContext httpContext);
    }
}
