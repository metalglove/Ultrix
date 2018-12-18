using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Presentation.ViewModels;
using Ultrix.Presentation.ViewModels.Meme;

namespace Ultrix.Presentation.Controllers
{
    public class MemeController : Controller
    {
        private readonly IMemeService _memeService;

        public MemeController(IMemeService memeService)
        {
            _memeService = memeService;
        }

        [Route("")]
        public async Task<IActionResult> IndexAsync()
        {
            //IEnumerable<MemeDto> memes = await _memeService.GetRandomMemesAsync(3);
            //return View("Index", memes.ToList());
            return View("Index", new List<MemeDto>());
        }

        [Route("Meme")]
        public async Task<IActionResult> MemeAsync()
        {
            MemeDto meme = await _memeService.GetRandomMemeAsync();
            return PartialView("Meme", meme);
        }

        [Route("Trending")]
        public async Task<IActionResult> TrendingAsync()
        {
            return View("Trending");
        }

        [Route("Like"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MemeLikeDto memeLikeDto = memeLikeViewModel.GetMemeLikeDto(userId);
            if(await _memeService.LikeMemeAsync(memeLikeDto))
            {
                return Json(new { success = true, liked = true });
            }

            return Json(new { success = false, message = "Something happened.." });
        }
        [Route("UnLike"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnLikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _memeService.UnLikeMemeAsync(memeLikeViewModel.MemeId, userId))
            {
                return Json(new { success = true, liked = false });
            }

            return Json(new { success = false, message = "Something happened.." });
        }
        [Route("Dislike"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DislikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MemeLikeDto memeLikeDto = memeLikeViewModel.GetMemeLikeDto(userId);
            if (await _memeService.DislikeMemeAsync(memeLikeDto))
            {
                return Json(new { success = true, disliked = true });
            }

            return Json(new { success = false, message = "Something happened.." });
        }
        [Route("UnDislike"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnDislikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _memeService.UnDislikeMemeAsync(memeLikeViewModel.MemeId, userId))
            {
                return Json(new { success = true, disliked = false });
            }

            return Json(new { success = false, message = "Something happened.." });
        }
        [Route("ShareMemeToFriend"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ShareMemeToFriendAsync([FromBody] ShareMemeViewModel shareMemeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            SharedMemeDto sharedMemeDto = shareMemeViewModel.GetSharedMemeDto(userId);
            SharedMemeResultDto sharedMemeResultDto = await _memeService.ShareMemeToFriendAsync(sharedMemeDto);
            return sharedMemeResultDto.Success 
                ? Json(new { success = true, to = sharedMemeResultDto.ReceiverUsername }) 
                : Json(new { success = false, message = "Something happened.." });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}