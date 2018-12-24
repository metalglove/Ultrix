using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("Users"), HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<ApplicationUserDto> users = await _userService.GetUsersAsync();
            return View("Users", new UsersViewModel(users));
        }

        [Route("Followers"), HttpGet]
        public async Task<IActionResult> GetFollowers()
        {
            throw new NotImplementedException();
        }

        [Route("Followings"), HttpGet]
        public async Task<IActionResult> GetFollowings()
        {
            throw new NotImplementedException();
        }

        [Route("MutualFollowings"), HttpGet]
        public async Task<IActionResult> GetMutualFollowings()
        {
            throw new NotImplementedException();
        }
    }
}
