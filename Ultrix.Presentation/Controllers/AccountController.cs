using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Contexts;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Ultrix.Presentation.Controllers
{
    public class AccountController : Controller
    {
        protected ApplicationDbContext _context;
        protected UserManager<ApplicationUser> _userManager;
        protected SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Route("ACC")]
        public IActionResult Index()
        {
            return View("../Home/Index");
        }

        [Route("create")]
        public async Task<IActionResult> CreateUserAsync()
        {
            IdentityResult createIdentityResult = await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Metalglove",
                Email = "metalglove@ultrix.nl"
            },"password");

            return Content(createIdentityResult.Succeeded ? "User was created" : "User creation failed", "text/html");
        }

        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Content("done");
        }

        [Route("login")]
        public async Task<IActionResult> LoginUserAsync(string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            SignInResult signInResult = await _signInManager.PasswordSignInAsync("Metalglove", "password", true, false);
            if (!signInResult.Succeeded)
                return Content("Failed to login", "text/html");

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction(nameof(Index));

            return Redirect(returnUrl);
        }

        [Authorize()]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"This is a private area. Welcome {HttpContext.User.Identity.Name}", "text/html");
        }
    }
}
