using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Presentation.ViewModels.Follower;

namespace Ultrix.Presentation.Controllers
{
    public class FollowerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFollowerService _followerService;
        private readonly ITempDataService _tempDataService;

        public FollowerController(
            IUserService userService,
            IFollowerService followerService,
            ITempDataService tempDataService)
        {
            _userService = userService;
            _followerService = followerService;
            _tempDataService = tempDataService;
        }

        [Route("Users"), HttpGet, Authorize]
        public async Task<IActionResult> UsersAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<FilteredApplicationUserDto> users = await _userService.GetFilteredUsersAsync(userId);
            return View("Users", new UsersViewModel(users));
        }

        [Route("Followers"), HttpGet, Authorize]
        public async Task<IActionResult> FollowersAsync()
        {
            // my followers
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<FollowerDto> followers = await _followerService.GetFollowersByUserIdAsync(userId);
            return View("Followers", new FollowersViewModel(followers));
        }

        [Route("Followings"), HttpGet, Authorize]
        public async Task<IActionResult> FollowingsAsync()
        {
            // who the user follows
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<FollowerDto> followers = await _followerService.GetFollowsByUserIdAsync(userId);
            return View("Followings", new FollowingsViewModel(followers));
        }

        [Route("MutualFollowings"), HttpGet, Authorize]
        public async Task<IActionResult> MutualFollowingsAsync()
        {
            // when the user also follows the follower, "Mutual"
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<FollowerDto> followers = await _followerService.GetMutualFollowersByUserIdAsync(userId);
            return View("Mutuals", new MutualsViewModel(followers));
        }

        [Route("Follow"), HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> FollowAsync([FromBody] FollowViewModel followViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Something happened try again later.." });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            FollowerDto follower = followViewModel.GetFollowerDto(userId);
            ServiceResponseDto followResultDto = await _followerService.FollowUserAsync(follower);
            await _tempDataService.UpdateTempDataAsync(TempData, userId);
            return Json(followResultDto);
        }
        [Route("UnFollow"), HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnFollowAsync([FromBody] UnFollowViewModel unFollowViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Something happened try again later.." });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            FollowerDto follower = unFollowViewModel.GetFollowerDto(userId);
            ServiceResponseDto unFollowResultDto = await _followerService.UnFollowUserAsync(follower);
            await _tempDataService.UpdateTempDataAsync(TempData, userId);
            return Json(unFollowResultDto);
        }
    }
}
