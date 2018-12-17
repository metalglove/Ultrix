﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Presentation.DTOs;
using Ultrix.Presentation.Utilities;
using Ultrix.Presentation.ViewModels.Account;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Ultrix.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFollowerService _followerService;
        private readonly ICollectionService _collectionService;

        public AccountController(
            IUserService userService, 
            IFollowerService followerService, 
            ICollectionService collectionService)
        {
            _userService = userService;
            _followerService = followerService;
            _collectionService = collectionService;
        }

        [Route("Register"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false });

            RegisterUserDto registerUserDto = registerViewModel.GetApplicationUserDto();
            IdentityResult createIdentityResult = await _userService.RegisterUserAsync(registerUserDto);
            return Json(createIdentityResult.Succeeded 
                ? new { success = true } 
                : new { success = false });
        }

        [Route("Logout"), Authorize]
        public async Task<IActionResult> LogoutAsync(string returnUrl)
        {
            await _userService.SignOutAsync(HttpContext);

            return RedirectToAction("IndexAsync", "Meme");
        }

        [Route("Login"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false });

            await _userService.SignOutAsync(HttpContext);
            LoginUserDto loginUserDto = loginViewModel.GetLoginUserDto();
            SignInResult signInResult = await _userService.SignInAsync(loginUserDto);

            if (!signInResult.Succeeded)
                return Json(new { success = false });

            int userId = await _userService.GetUserIdByUserNameAsync(loginViewModel.Username);
            List<CollectionDTO> collectionDTOs = (await _collectionService.GetMyCollectionsAsync(userId))
                .Select(collection => new CollectionDTO { Name = collection.Name, Id = collection.Id })
                .ToList();
            List<FollowingDto> mutualFollowingsDTOs = await _followerService.GetMutualFollowingsByUserIdAsync(userId);
            TempData.Put("collections", collectionDTOs);
            TempData.Keep("collections");
            TempData.Put("mutualFollowings", mutualFollowingsDTOs);
            TempData.Keep("mutualFollowings");

            return Json(new { success = true, loggedin = true });
        }

        [Authorize]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"This is a private area. Welcome {HttpContext.User.Identity.Name}", "text/html");
        }
    }
}
