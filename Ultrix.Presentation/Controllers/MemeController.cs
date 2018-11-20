using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Presentation.ViewModels;

namespace Ultrix.Presentation.Controllers
{
    public class MemeController : Controller
    {
        private readonly IMemeService _memeService;
        private readonly IMemeRepository _memeRepository;

        // Switch between IExternalMemeService and ILocalMemeService both implement IMemeService
        public MemeController(ILocalMemeService memeService, IMemeRepository memeRepository)
        {
            _memeService = memeService;
            _memeRepository = memeRepository;
        }

        [Route("")]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Meme> memes = new[]
            {
                await _memeService.GetRandomMemeAsync(), await _memeService.GetRandomMemeAsync(),
                await _memeService.GetRandomMemeAsync(), await _memeService.GetRandomMemeAsync()
            };
            return View("Index", memes);
        }

        [Route("Meme")]
        public async Task<IActionResult> MemeAsync()
        {
            Meme meme = await _memeService.GetRandomMemeAsync();
            return PartialView("Meme", meme);
        }

        [Route("Trending")]
        public IActionResult Trending()
        {
            return View("Trending");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}