using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;

namespace Ultrix.Application.Services
{
    public class TempDataService : ITempDataService
    {
        private readonly ICollectionService _collectionService;
        private readonly IFollowerService _followerService;

        public TempDataService(ICollectionService collectionService, IFollowerService followerService)
        {
            _collectionService = collectionService;
            _followerService = followerService;
        }

        public async Task UpdateTempDataAsync(ITempDataDictionary tempData, int userId)
        {
            IEnumerable<ShareCollectionDto> collectionDTOs = (await _collectionService.GetMyCollectionsAsync(userId))
                .Select(collection => new ShareCollectionDto { Name = collection.Name, Id = collection.Id });
            IEnumerable<FollowerDto> mutualFollowingsDTOs = await _followerService.GetMutualFollowingsByUserIdAsync(userId);
            tempData["collections"] = JsonConvert.SerializeObject(collectionDTOs.ToList());
            tempData.Keep("collections");
            tempData["mutualFollowings"] = JsonConvert.SerializeObject(mutualFollowingsDTOs.ToList());
            tempData.Keep("mutualFollowings");
        }
    }
}
