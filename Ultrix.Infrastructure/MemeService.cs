using System;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Infrastructure.Utilities;
using Ultrix.Domain.Entities;
using Newtonsoft.Json;

namespace Ultrix.Infrastructure
{
    public class MemeService : WebServiceBase, IMemeService
    {
        private static readonly Uri ultrix_meme_api = new Uri("http://ultrix.nl/Api/");

        public MemeService() : base(ultrix_meme_api)
        {

        }

        public async Task<Meme> GetRandomMemeAsync()
        {
            string memeAsJson = await GetAsync("RandomMeme.php");
            Meme meme = JsonConvert.DeserializeObject<Meme>(memeAsJson, new MemeConverter());
            return meme == null || meme.Title == null ? await GetRandomMemeAsync() : meme;
        }
    }
}
