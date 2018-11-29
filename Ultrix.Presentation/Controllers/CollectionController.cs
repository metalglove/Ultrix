using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
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
        public async Task<IActionResult> CollectionsAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Collection> collections = await _collectionRepository.GetAllCollectionsAsync();
            return View("Collections", collections);
        }
        [Route("Collection/{id}"), HttpGet]
        public async Task<IActionResult> CollectionAsync(int? id)
        {
            if(!id.HasValue)
                return BadRequest();
            Collection collection = await _collectionRepository.GetCollectionAsync(id.Value);
            if (collection.Equals(default))
                return BadRequest();
            else
                return View("Collection", new CollectionViewModel(collection, false));
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
        [Route("MyCollections"), Authorize, HttpGet]
        public async Task<IActionResult> MyCollectionsAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Collection> myCollections = await _collectionRepository.GetMyCollectionsAsync(userId);
            List<Collection> subcribedCollections = await _collectionRepository.GetMySubscribedCollectionsAsync(userId);
            MyCollectionsViewModel myCollectionViewModel = new MyCollectionsViewModel(myCollections, subcribedCollections);
            return View("MyCollections", myCollectionViewModel);
        }

        #region Dev
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

        [Route("AddMemeToCollection")]
        public async Task<IActionResult> AddMemeToCollectionAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Meme meme = await _memeRepository.GetMemeAsync("aPYWWvV");
            await _collectionRepository.AddToCollectionAsync(meme, 1, userId);

            Meme meme2 = await _memeRepository.GetMemeAsync("arG4qNd");
            await _collectionRepository.AddToCollectionAsync(meme2, 1, userId);

            Meme meme3 = await _memeRepository.GetMemeAsync("az9Lr5K");
            await _collectionRepository.AddToCollectionAsync(meme3, 1, userId);

            Meme meme4 = await _memeRepository.GetMemeAsync("aVYWpyy");
            await _collectionRepository.AddToCollectionAsync(meme4, 1, userId);

            return Content("ok", "text/html");
        }
        #endregion Dev
    }
}