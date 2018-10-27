using System;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Infrastructure.Utilities;
using Ultrix.Domain.Entities;
using Newtonsoft.Json;

namespace Ultrix.Infrastructure
{
    public class ExternalMemeService : WebServiceBase, IExternalMemeService
    {
        private static readonly Uri UltrixMemeApi = new Uri("http://ultrix.nl/Api/");

        public ExternalMemeService() : base(UltrixMemeApi)
        {

        }

        public async Task<Meme> GetRandomMemeAsync()
        {
            string memeAsJson = await GetAsync("RandomMeme.php");
            Meme meme = JsonConvert.DeserializeObject<Meme>(memeAsJson, new MemeConverter());
            return meme?.Title == null ? await GetRandomMemeAsync() : meme;
        }
    }
}
