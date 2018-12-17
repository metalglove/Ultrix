using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Infrastructure.Extensions;

namespace Ultrix.Infrastructure.Services
{
    public class LocalMemeFetcherService : ILocalMemeFetcherService
    {
        private const string NineGagRandomUrl = "http://9gag.com/random/";
        private const string NineGagPhotoCacheUrl = "http://img-9gag-fun.9cache.com/photo/";
        private const string Mp4Tag = "_460sv.mp4";

        public async Task<MemeDto> GetRandomMemeAsync()
        {
            return await GetMemeAsync();
        }
        private async Task<MemeDto> GetMemeAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(NineGagRandomUrl, HttpCompletionOption.ResponseHeadersRead);
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(stream);
                    IEnumerable<HtmlNode> collection = ExtractValidMetaTags(doc);
                    return await ExtractMemeFromMetaNodesAsync(collection);
                }
            }
        }
        private static IEnumerable<HtmlNode> ExtractValidMetaTags(HtmlDocument doc)
        {
            return doc.DocumentNode
                .SelectNodes("//meta/@content")
                    .Where(node =>
                        node.Attributes["property"] != null &&
                        node.Attributes["property"].Value.StartsWith("og:") &&
                        node.Attributes["content"] != null);
        }
        private static async Task<MemeDto> ExtractMemeFromMetaNodesAsync(IEnumerable<HtmlNode> collection)
        {
            MemeDto meme = new MemeDto();
            foreach (HtmlNode htmlNode in collection)
            {
                string content = htmlNode.Attributes["content"].DeEntitizeValue;
                switch (htmlNode.Attributes["property"].DeEntitizeValue)
                {
                    case "og:title":
                        meme.Title = content;
                        break;
                    case "og:url":
                        meme.PageUrl = content;
                        break;
                    case "og:image":
                        meme.ImageUrl = content;
                        break;
                    default:
                        break;
                }
            }
            meme.Id = meme.GetMemeIdFromUrl();
            string videoUrl = NineGagPhotoCacheUrl + meme.Id + Mp4Tag;
            if (await HasVideoUrlAsync(videoUrl))
            {
                meme.VideoUrl = videoUrl;
            }
            return meme;
        }
        private static async Task<bool> HasVideoUrlAsync(string videoUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage newMessage = await httpClient.GetAsync(videoUrl, HttpCompletionOption.ResponseHeadersRead);
                return newMessage.StatusCode.Equals(HttpStatusCode.OK);
            }
        }
    }
}
