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
                return Json(new { success = true, Message = "Meme is liked." });
            }

            //if (await _memeRepository.DoesMemeExistAsync(memeId))
            //    return Json(new { success = false, Message = "Meme does not exist." });

            //return isLike
            //    ? isLiked
            //        ? await _memeRepository.LikeMemeAsync(memeId)
            //            ? Json(new { success = true, Message = "Meme is liked." })
            //            : Json(new { success = false, Message = "Meme is not liked." })
            //        : await _memeRepository.UnLikeMemeAsync(memeId)
            //            ? Json(new { success = true, Message = "Meme is unliked." })
            //            : Json(new { success = false, Message = "Meme is not unliked." })
            //    : isLiked
            //        ? await _memeRepository.DisLikeMemeAsync(memeId)
            //                ? Json(new { success = true, Message = "Meme is disliked." })
            //                : Json(new { success = false, Message = "Meme is not disliked." })
            //        : await _memeRepository.UnDisLikeMemeAsync(memeId)
            //            ? Json(new { success = true, Message = "Meme is undisliked." })
            //            : Json(new { success = false, Message = "Meme is not undisliked." });
            return Json(new { success = false, Message = "Meme does not exist." });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}