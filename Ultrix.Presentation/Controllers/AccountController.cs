using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Presentation.ViewModels.Account;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Ultrix.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("ACC")]
        public IActionResult Index()
        {
            return View("../Home/Index");
        }

        [Route("Register")]
        public async Task<IActionResult> CreateUserAsync()
        {
            IdentityResult createIdentityResult = await _userService.CreateUserAsync(new ApplicationUser
            {
                UserName = "Metalglove",
                Email = "metalglove@ultrix.nl",
                UserDetail = new UserDetail { ProfilePictureData = "test" }
            }, "password");

            return Content(createIdentityResult.Succeeded ? "User was created" : "User creation failed", "text/html");
        }

        [Route("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync(HttpContext);
            return Content("done");
        }

        [Route("Login"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                await _userService.SignOutAsync(HttpContext);
                SignInResult signInResult = await _userService.SignInAsync(loginViewModel.Username, loginViewModel.Password);
                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                    {
                        return Json(new { Authenticated = true, ReturnUrl = loginViewModel.ReturnUrl });
                    }

                    return Json(new { Authenticated = true });
                }
            }
            
            return Json(new { Authenticated = false });
        }

        [Authorize()]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"This is a private area. Welcome {HttpContext.User.Identity.Name}", "text/html");
        }
    }
}
