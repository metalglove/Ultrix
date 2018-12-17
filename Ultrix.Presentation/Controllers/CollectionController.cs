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
            return View("Collection", new CollectionViewModel(collection, false));
        }
        [Route("CreateCollection"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CreateCollectionViewModel createCollectionViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new {IsCreated = false});
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
        [Route("AddMemeToCollection"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemeToCollectionAsync(AddMemeToCollectionViewModel addMemeToCollectionViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { IsCreated = false, Message = "ModelState is not valid." });

            string memeId = addMemeToCollectionViewModel.MemeId;
            int collectionId = addMemeToCollectionViewModel.CollectionId;

            if (!await _memeRepository.DoesMemeExistAsync(memeId))
                return Json(new { IsCreated = false, Message = "Meme does not exist." });

            if (!await _collectionRepository.DoesCollectionExistAsync(collectionId))
                return Json(new { IsCreated = false, Message = "Collection does not exist." });

            if (!await _collectionRepository.DoesMemeExistInCollectionAsync(memeId, collectionId))
                return Json(new { IsCreated = false, Message = "Duplicate" });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Meme meme = await _memeRepository.GetMemeAsync(addMemeToCollectionViewModel.MemeId);
            await _collectionRepository.AddToCollectionAsync(meme, collectionId, userId);

            return Json(new { IsCreated = true });
        }
        [Route("MyCollections"), Authorize, HttpGet]
        public async Task<IActionResult> MyCollectionsAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Collection> myCollections = await _collectionRepository.GetMyCollectionsAsync(userId);
            List<Collection> subscribedCollections = await _collectionRepository.GetMySubscribedCollectionsAsync(userId);
            MyCollectionsViewModel myCollectionViewModel = new MyCollectionsViewModel(myCollections, subscribedCollections);
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

        
        #endregion Dev
    }
}