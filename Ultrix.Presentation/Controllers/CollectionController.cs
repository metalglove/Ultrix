using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Presentation.ViewModels.Collection_;

namespace Ultrix.Presentation.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly ICollectionItemDetailService _collectionItemDetailService;
        private readonly ICollectionSubscriberService _collectionSubscriberService;

        public CollectionController(
            ICollectionService collectionService, 
            ICollectionItemDetailService collectionItemDetailService, 
            ICollectionSubscriberService collectionSubscriberService)
        {
            _collectionService = collectionService;
            _collectionItemDetailService = collectionItemDetailService;
            _collectionSubscriberService = collectionSubscriberService;
        }

        [Route("Collections")]
        public async Task<IActionResult> CollectionsAsync()
        {
            IEnumerable<CollectionDto> collectionsDtos = await _collectionService.GetAllCollectionsAsync();
            return View("Collections", collectionsDtos);
        }
        [Route("Collection/{id}"), HttpGet]
        public async Task<IActionResult> CollectionAsync(int? id)
        {
            if(!id.HasValue)
                return BadRequest();
            CollectionDto collectionDto = await _collectionService.GetCollectionByIdAsync(id.Value);
            if (collectionDto.Equals(default))
                return BadRequest();
            return View("Collection", new CollectionViewModel(collectionDto, false));
        }
        [Route("CreateCollection"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CreateCollectionViewModel createCollectionViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new {Success = false, Message = "Something happend try again later.." });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CollectionDto collection = createCollectionViewModel.GetCollectionDto(userId);
            return Json(await _collectionService.CreateCollectionAsync(collection));
        }
        [Route("AddMemeToCollection"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemeToCollectionAsync([FromBody] AddMemeToCollectionViewModel addMemeToCollectionViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Something happend try again later.." });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            AddMemeToCollectionDto addMemeToCollectionDto =
                addMemeToCollectionViewModel.GetAddMemeToCollectionDto(userId);

            return Json(await _collectionItemDetailService.AddMemeToCollectionAsync(addMemeToCollectionDto));
        }
        [Route("MyCollections"), Authorize, HttpGet]
        public async Task<IActionResult> MyCollectionsAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<CollectionDto> myCollections = await _collectionService.GetMyCollectionsAsync(userId);
            IEnumerable<CollectionDto> subscribedCollections = await _collectionSubscriberService.GetMySubscribedCollectionsAsync(userId);
            MyCollectionsViewModel myCollectionViewModel = new MyCollectionsViewModel(myCollections, subscribedCollections);
            return View("MyCollections", myCollectionViewModel);
        }
    }
}