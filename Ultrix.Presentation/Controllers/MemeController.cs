using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Enumerations;
using Ultrix.Presentation.Utilities;
using Ultrix.Presentation.ViewModels;
using Ultrix.Presentation.ViewModels.Meme;

namespace Ultrix.Presentation.Controllers
{
    public class MemeController : Controller
    {
        private readonly IMemeService _memeService;
        private readonly IMemeSharingService _memeSharingService;
        private readonly IMemeLikingService _memeLikingService;

        public MemeController(
            IMemeService memeService, 
            IMemeSharingService memeSharingService,
            IMemeLikingService memeLikingService)
        {
            _memeService = memeService;
            _memeSharingService = memeSharingService;
            _memeLikingService = memeLikingService;
        }

        [Route("")]
        public Task<IActionResult> IndexAsync()
        {
            return Task.FromResult((IActionResult)base.View("Index", new List<MemeDto>()));
        }

        [Route("Meme")]
        public async Task<IActionResult> MemeAsync()
        {
            MemeDto meme = await _memeService.GetRandomMemeAsync();
            return PartialView("Meme", meme);
        }

        [Route("Trending")]
        public Task<IActionResult> TrendingAsync()
        {
            return Task.FromResult((IActionResult)base.View("Trending"));
        }

        [Route("Like"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.DefaultInvalidModelStateWithErrorMessages());

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MemeLikeDto memeLikeDto = memeLikeViewModel.GetMemeLikeDto(userId);
            return await _memeLikingService.LikeMemeAsync(memeLikeDto)
                ? Json(new {success = true, liked = true})
                : Json(new {success = false, message = "Something happened.."});
        }
        [Route("UnLike"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnLikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.DefaultInvalidModelStateWithErrorMessages());

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _memeLikingService.UnLikeMemeAsync(memeLikeViewModel.MemeId, userId)
                ? Json(new {success = true, liked = false})
                : Json(new {success = false, message = "Something happened.."});
        }
        [Route("Dislike"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DislikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.DefaultInvalidModelStateWithErrorMessages());

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MemeLikeDto memeLikeDto = memeLikeViewModel.GetMemeLikeDto(userId);
            return await _memeLikingService.DislikeMemeAsync(memeLikeDto)
                ? Json(new {success = true, disliked = true})
                : Json(new {success = false, message = "Something happened.."});
        }
        [Route("UnDislike"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UnDislikeAsync([FromBody] MemeLikeViewModel memeLikeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.DefaultInvalidModelStateWithErrorMessages());

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _memeLikingService.UnDislikeMemeAsync(memeLikeViewModel.MemeId, userId)
                ? Json(new {success = true, disliked = false})
                : Json(new {success = false, message = "Something happened.."});
        }
        [Route("ShareMemeToFriend"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ShareMemeToFriendAsync([FromBody] ShareMemeViewModel shareMemeViewModel)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.DefaultInvalidModelStateWithErrorMessages());

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            SharedMemeDto sharedMemeDto = shareMemeViewModel.GetSharedMemeDto(userId);
            ServiceResponseDto sharedMemeResultDto = await _memeSharingService.ShareMemeToMutualFollowerAsync(sharedMemeDto);
            return sharedMemeResultDto.Success 
                ? Json(new {success = true, to = sharedMemeResultDto.Message}) 
                : Json(new {success = false, message = "Something happened.."});
        }
        [Route("SharedMemes"), Authorize, HttpGet]
        public async Task<IActionResult> SharedMemesAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<SharedMemeDto> sharedMemes = await _memeSharingService.GetSharedMemesAsync(userId, SeenStatus.Any);
            return View("SharedMemes", new SharedMemesViewModel(sharedMemes));
        }
        [Route("Meme/{id}"), HttpGet]
        public async Task<IActionResult> MemeByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            // TODO: fix
            try
            {
                MemeDto meme = await _memeService.GetMemeAsync(id);
                return View("SharedMemeExpanded", meme);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Route("MarkMemeAsSeen"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkMemeAsSeenAsync([FromBody] MarkMemeAsSeenViewModel markMemeAsSeenViewModel)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.DefaultInvalidModelStateWithErrorMessages());

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            SharedMemeDto sharedMemeDto = markMemeAsSeenViewModel.GetSharedMemeDto(userId);
            ServiceResponseDto sharedMemeMarkAsSeenDto = await _memeSharingService.MarkSharedMemeAsSeenAsync(sharedMemeDto);
            return Json(sharedMemeMarkAsSeenDto);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}