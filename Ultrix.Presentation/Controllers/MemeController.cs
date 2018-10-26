using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

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
        public IActionResult Index()
        {
            return View();
        }

        [Route("Memes")]
        public async Task<IActionResult> Memes()
        {
            IEnumerable<Meme> memes = new Meme[]
            {
                await _memeService.GetRandomMemeAsync(),
                await _memeService.GetRandomMemeAsync(),
                await _memeService.GetRandomMemeAsync(),
                await _memeService.GetRandomMemeAsync()
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
