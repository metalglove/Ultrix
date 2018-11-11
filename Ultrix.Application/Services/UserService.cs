using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Ultrix.Application.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task<SignInResult> SignInAsync(string userName, string password)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, true, false);
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
    }
}
