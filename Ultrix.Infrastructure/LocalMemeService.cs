using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Infrastructure.Extensions;

namespace Ultrix.Infrastructure
{
    public class LocalMemeService : ILocalMemeService
    {
        private const string NineGagRandomUrl = "http://9gag.com/random/";
        private const string NineGagPhotoCacheUrl = "http://img-9gag-fun.9cache.com/photo/";
        private const string Mp4Tag = "_460sv.mp4";
        private readonly IMemeRepository _memeRepository;

        public LocalMemeService(IMemeRepository memeRepository)
        {
            _memeRepository = memeRepository;
        }

        public async Task<Meme> GetRandomMemeAsync()
        {
            // TODO: multi-thread fetch memes?..
            Meme meme = await GetMemeAsync();
            while (await _memeRepository.DoesMemeExistAsync(meme))
            {
                meme = await GetMemeAsync();
            }
            return meme;
        }
        private async Task<Meme> GetMemeAsync()
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
        private static async Task<Meme> ExtractMemeFromMetaNodesAsync(IEnumerable<HtmlNode> collection)
        {
            Meme meme = new Meme();
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
            //meme.TimestampAdded = DateTime.Now;
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
