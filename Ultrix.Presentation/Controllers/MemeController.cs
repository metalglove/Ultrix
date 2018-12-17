using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
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
            IEnumerable<MemeDto> memes = await _memeService.GetRandomMemesAsync(4);
            return View("Index", memes);
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

        [Route("Like"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, Message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MemeLikeDto memeLikeDto = memeLikeViewModel.GetMemeLikeDto(userId);
            if(await _memeService.LikeMemeAsync(memeLikeDto))
            {
                return Json(new { success = true, Liked = true });
            }

            return Json(new { success = false, Message = "Something happened.." });
        }
        [Route("UnLike"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnLikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, Message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _memeService.UnLikeMemeAsync(memeLikeViewModel.MemeId, userId))
            {
                return Json(new { success = true, Liked = false });
            }

            return Json(new { success = false, Message = "Something happened.." });
        }
        [Route("Dislike"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DislikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, Message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MemeLikeDto memeLikeDto = memeLikeViewModel.GetMemeLikeDto(userId);
            if (await _memeService.DislikeMemeAsync(memeLikeDto))
            {
                return Json(new { success = true, Disliked = true });
            }

            return Json(new { success = false, Message = "Something happened.." });
        }
        [Route("UnDislike"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnDislikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, Message = "Invalid" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _memeService.UnLikeMemeAsync(memeLikeViewModel.MemeId, userId))
            {
                return Json(new { success = true, Disliked = false });
            }

            return Json(new { success = false, Message = "Something happened.." });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}