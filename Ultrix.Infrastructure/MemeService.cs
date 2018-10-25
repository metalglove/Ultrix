using System;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Common;
using Ultrix.Infrastructure.Utilities;
using Ultrix.Infrastructure.Entities;

namespace Ultrix.Infrastructure
{
    public class MemeService : WebServiceBase, IMemeService
    {
        private static readonly Uri ultrix_meme_api = new Uri("http://ultrix.nl/Api/");

        public MemeService() : base(ultrix_meme_api)
        {

        }

        public async Task<IMeme> GetRandomMemeAsync()
        {
            IMeme meme = await GetJsonAsyncAndConvertTo<Meme>("RandomMeme.php");
            return meme == null || meme.Title == null ? await GetRandomMemeAsync() : meme;
        }
    }
}
