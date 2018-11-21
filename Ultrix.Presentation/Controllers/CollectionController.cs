using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Presentation.ViewModels.Collection_;

namespace Ultrix.Presentation.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMemeRepository _memeRepository;

        public CollectionController(ICollectionRepository collectionRepository, IMemeRepository memeRepository)
        {
            _collectionRepository = collectionRepository;
            _memeRepository = memeRepository;
        }

        [Route("Collections")]
        public IActionResult Collections()
        {
            return View("Collections");
        }

        [Route("CreateCollection"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CreateCollectionViewModel createCollectionViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _collectionRepository.DoesCollectionNameExistAsync(createCollectionViewModel.Name))
                    return Json(new { IsCreated = false });

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Collection collection = new Collection
                {
                    Name = createCollectionViewModel.Name,
                    UserId = Convert.ToInt32(userId)
                };

                if (await _collectionRepository.CreateCollectionAsync(collection))
                    return Json(new { IsCreated = true });
            }
            return Json(new { IsCreated = false });
        }

        [Route("CreateCollection2"), Authorize, HttpGet]
        public async Task<IActionResult> CreateCollectionAsync2()
        {
            CreateCollectionViewModel createCollectionViewModel = new CreateCollectionViewModel()
            {
                Name = "Dank Memes"
            };
            if (await _collectionRepository.DoesCollectionNameExistAsync(createCollectionViewModel.Name))
                return Json(new { IsCreated = false });

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Collection collection = new Collection
            {
                Name = createCollectionViewModel.Name,
                UserId = Convert.ToInt32(userId)
            };

            if (await _collectionRepository.CreateCollectionAsync(collection))
                return Json(new { IsCreated = true });
            return Json(new { IsCreated = false });
        }
        [Route("MyCollections"), Authorize, HttpGet]
        public async Task<IActionResult> MyCollectionsAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Collection> myCollections = await _collectionRepository.GetMyCollectionsAsync(userId);
            List<Collection> subcribedCollections = await _collectionRepository.GetMySubscribedCollectionsAsync(userId);
            MyCollectionsViewModel myCollectionViewModel = new MyCollectionsViewModel(myCollections, subcribedCollections);
            return View("MyCollections", myCollectionViewModel);
        }

        [Route("AddMemeToCollection")]
        public async Task<IActionResult> AddMemeToCollectionAsync()
        {
            Meme meme = await _memeRepository.GetMemeAsync("aLgPGDM");
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _collectionRepository.AddToCollectionAsync(meme, 1, userId);
            return Content("ok", "text/html");
        }
    }
}