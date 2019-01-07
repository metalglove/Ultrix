using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Presentation.Utilities;
using Ultrix.Presentation.ViewModels.Account;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Ultrix.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITempDataService _tempDataService;

        public AccountController(
            IUserService userService, 
            ITempDataService tempDataService)
        {
            _userService = userService;
            _tempDataService = tempDataService;
        }

        [Route("Register"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false });

            RegisterUserDto registerUserDto = registerViewModel.GetRegisterUserDto();
            SignUpResultDto createIdentityResult = await _userService.SignUpAsync(registerUserDto);
            return Json(createIdentityResult.Success 
                ? new { success = true } 
                : new { success = false });
        }

        [Route("Logout"), Authorize]
        public async Task<IActionResult> LogoutAsync(string returnUrl)
        {
            await _userService.SignOutAsync();
            return RedirectToAction("IndexAsync", "Meme");
        }

        [Route("Login"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false });

            await _userService.SignOutAsync();
            LoginUserDto loginUserDto = loginViewModel.GetLoginUserDto();
            SignInResultDto signInResult = await _userService.SignInAsync(loginUserDto);

            if (!signInResult.Success)
                return Json(new { success = false });

            int userId = await _userService.GetUserIdByEmailAsync(loginViewModel.Email);
            await _tempDataService.UpdateTempDataAsync(TempData, userId);

            return Json(new { success = true, loggedin = true });
        }

        [Route("VerifyTempData"), HttpGet]
        public async Task<IActionResult> VerifyTempDataAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new {success = false, refresh = false});

            TempData.Peek("collections", out List<ShareCollectionDto> collections);
            TempData.Peek("mutualFollowings", out List<FollowerDto> mutualFollowings);

            if (collections != null && mutualFollowings != null)
                return Json(new { success = true, refresh = false });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _tempDataService.UpdateTempDataAsync(TempData, userId);

            return Json(new { success = true, refresh = true });
        }
    }
}
