using Ultrix.Domain.Entities;

namespace Ultrix.Infrastructure.Extensions
{
    internal static class MemeExtensions
    {
        private const string NineGagGagUrl = "http://9gag.com/gag/";

        internal static string GetMemeIdFromUrl(this Meme meme)
        {
            return meme.PageUrl.Replace(NineGagGagUrl, "");
        }
    }
}
