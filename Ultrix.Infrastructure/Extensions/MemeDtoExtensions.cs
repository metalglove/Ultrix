using Ultrix.Application.DTOs;

namespace Ultrix.Infrastructure.Extensions
{
    internal static class MemeDtoExtensions
    {
        private const string NineGagGagUrl = "http://9gag.com/gag/";

        internal static string GetMemeIdFromUrl(this MemeDto meme)
        {
            return meme.PageUrl.Replace(NineGagGagUrl, "");
        }
    }
}
