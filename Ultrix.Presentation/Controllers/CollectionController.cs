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

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [Route("Collections")]
        public async Task<IActionResult> CollectionsAsync()
        {
            List<CollectionDto> collectionsDtos = await _collectionService.GetAllCollectionsAsync();
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
                return Json(new {IsCreated = false});

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CollectionDto collection = createCollectionViewModel.GetCollectionDto(userId);
            return await _collectionService.CreateCollectionAsync(collection)
                ? Json(new {IsCreated = true})
                : Json(new {IsCreated = false});
        }
        [Route("AddMemeToCollection"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemeToCollectionAsync([FromBody] AddMemeToCollectionViewModel addMemeToCollectionViewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { IsCreated = false, Message = "ModelState is not valid." });

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            AddMemeToCollectionDto addMemeToCollectionDto =
                addMemeToCollectionViewModel.GetAddMemeToCollectionDto(userId);
            return await _collectionService.AddToCollectionAsync(addMemeToCollectionDto)
                ? Json(new {IsCreated = true})
                : Json(new {IsCreated = false});
        }
        [Route("MyCollections"), Authorize, HttpGet]
        public async Task<IActionResult> MyCollectionsAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<CollectionDto> myCollections = await _collectionService.GetMyCollectionsAsync(userId);
            List<CollectionDto> subscribedCollections = await _collectionService.GetMySubscribedCollectionsAsync(userId);
            MyCollectionsViewModel myCollectionViewModel = new MyCollectionsViewModel(myCollections, subscribedCollections);
            return View("MyCollections", myCollectionViewModel);
        }
    }
}