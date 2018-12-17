using System;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Infrastructure.Utilities;
using Newtonsoft.Json;
using Ultrix.Infrastructure.Extensions;
using Ultrix.Application.DTOs;

namespace Ultrix.Infrastructure.Services
{
    public class ExternalMemeFetcherService : WebServiceBase, IExternalMemeFetcherService
    {
        private static readonly Uri UltrixMemeApi = new Uri("http://ultrix.nl/Api/");

        public ExternalMemeFetcherService() : base(UltrixMemeApi)
        {

        }

        public async Task<MemeDto> GetRandomMemeAsync()
        {
            while (true)
            {
                string memeAsJson = await GetAsync("RandomMeme.php");
                MemeDto meme = JsonConvert.DeserializeObject<MemeDto>(memeAsJson, new MemeConverter());
                meme.Id = meme.GetMemeIdFromUrl();
                if (meme.Title == null) continue;
                return meme;
            }
        }
    }
}
