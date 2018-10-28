using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Presentation.Controllers
{
    public class MemeController : Controller
    {
        private readonly IMemeService _memeService;
        private readonly IMemeRepository _memeRepository;

        // Switch between IExternalMemeService and ILocalMemeService both implement IMemeService
        public MemeController(ILocalMemeService memeService, IMemeRepository memeRepository) // i do not want MemesDbContext here but rather IMemesDbContext
        {
            _memeService = memeService;
            _memeRepository = memeRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Memes")]
        public async Task<IActionResult> Memes()
        {
            IEnumerable<Meme> memes = new[]
            {
                await _memeService.GetRandomMemeAsync(), await _memeService.GetRandomMemeAsync(),
                await _memeService.GetRandomMemeAsync(), await _memeService.GetRandomMemeAsync()
            };
            return View("Memes", memes);
        }

        [Route("Meme")]
        public async Task<IActionResult> Meme()
        {
            Meme meme = await _memeService.GetRandomMemeAsync();
            return PartialView("Meme", meme);
        }
    }
}