using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Presentation.ViewModels.Collection;

namespace Ultrix.Presentation.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionController(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
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
                //
            }

            return View("Collections");
        }

        [Route("CreateCollection2"), Authorize]
        public async Task<IActionResult> CreateCollection2Async()
        {
            CreateCollectionViewModel createCollectionViewModel = new CreateCollectionViewModel
            {
                Name = "Dank Memes"
            };
            
            if (await _collectionRepository.DoesCollectionNameExistAsync(createCollectionViewModel.Name))
                return Json(new { IsCreated = false });

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Collection collection = new Collection
            {
                Name = createCollectionViewModel.Name,
                UserId = Convert.ToInt32(userId),
                TimestampAdded = DateTime.UtcNow
            };

            return await _collectionRepository.CreateCollectionAsync(collection)
                ? Json(new { IsCreated = true })
                : Json(new { IsCreated = false });
        }

        [Route("MyCollection"), Authorize, HttpGet]
        public async Task<IActionResult> MyCollectionsAsync()
        {
            string userName = HttpContext.User.Identity.Name;
            List<Collection> myCollections = await _collectionRepository.GetMyCollectionsAsync(userName);

            return View("Collections");
        }
    }
}