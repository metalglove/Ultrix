﻿using Microsoft.AspNetCore.Authorization;
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

        public FollowerController(
            IUserService userService,
            IFollowerService followerService)
        {
            _userService = userService;
            _followerService = followerService;
        }

        [Route("Users"), HttpGet, Authorize]
        public async Task<IActionResult> UsersAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<FilteredApplicationUserDto> users = await _userService.GetUsersAsync(userId);
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
            throw new NotImplementedException();
        }

        [Route("MutualFollowings"), HttpGet, Authorize]
        public async Task<IActionResult> MutualFollowingsAsync()
        {
            // when the user also follows the follower, "Mutual"
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<FollowerDto> followers = await _followerService.GetMutualFollowingsByUserIdAsync(userId);
            return View("Mutuals", new MutualsViewModel(followers));
        }

        [Route("Follow"), HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> FollowAsync([FromBody] FollowViewModel followViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Something happend try again later.." });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            FollowerDto follower = followViewModel.GetFollowerDto(userId);
            FollowResultDto followResultDto = await _followerService.FollowUserAsync(follower);
            return Json(followResultDto);
        }
        [Route("UnFollow"), HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnFollowAsync([FromBody] UnFollowViewModel unFollowViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Something happend try again later.." });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            FollowerDto follower = unFollowViewModel.GetFollowerDto(userId);
            UnFollowResultDto unFollowResultDto = await _followerService.UnFollowUserAsync(follower);
            return Json(unFollowResultDto);
        }
    }
}
