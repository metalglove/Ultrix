using System;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Infrastructure.Utilities;
using Ultrix.Domain.Entities;
using Newtonsoft.Json;
using Ultrix.Infrastructure.Extensions;

namespace Ultrix.Infrastructure.Services
{
    public class ExternalMemeService : WebServiceBase, IExternalMemeService
    {
        private static readonly Uri UltrixMemeApi = new Uri("http://ultrix.nl/Api/");

        public ExternalMemeService() : base(UltrixMemeApi)
        {

        }

        public async Task<Meme> GetRandomMemeAsync()
        {
            while (true)
            {
                string memeAsJson = await GetAsync("RandomMeme.php");
                Meme meme = JsonConvert.DeserializeObject<Meme>(memeAsJson, new MemeConverter());
                meme.Id = meme.GetMemeIdFromUrl();
                if (meme.Title == null) continue;
                return meme;
            }
        }
    }
}
