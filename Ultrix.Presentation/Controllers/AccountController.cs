using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
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
        private readonly ICollectionRepository _collectionRepository;

        public AccountController(IUserService userService, IFollowerService followerService, ICollectionRepository collectionRepository)
        {
            _userService = userService;
            _followerService = followerService;
            _collectionRepository = collectionRepository;
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
                return Json(new {authenticated = false});

            await _userService.SignOutAsync(HttpContext);
            SignInResult signInResult = await _userService.SignInAsync(loginViewModel.Username, loginViewModel.Password);

            if (!signInResult.Succeeded)
                return Json(new {authenticated = false});

            int userId = await _userService.GetUserIdByUserName(loginViewModel.Username);
            List<CollectionDTO> collectionDTOs = (await _collectionRepository.GetMyCollectionsAsync(userId))
                .Select(collection => new CollectionDTO { Name = collection.Name, Id = collection.Id })
                .ToList();
            List<FollowingDto> mutualFollowingsDTOs = await _followerService.GetMutualFollowingsByUserIdAsync(userId);
            TempData.Put("collections", collectionDTOs);
            TempData.Keep("collections");
            TempData.Put("mutualFollowings", mutualFollowingsDTOs);
            TempData.Keep("mutualFollowings");

            return !string.IsNullOrEmpty(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl)
                ? Json(new { authenticated = true, returnurl = loginViewModel.ReturnUrl })
                : Json(new { authenticated = true });
        }

        [Authorize]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"This is a private area. Welcome {HttpContext.User.Identity.Name}", "text/html");
        }
    }
}
