﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public LocalMemeService()
        {

        }

        public async Task<Meme> GetRandomMemeAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //const int bytesToRead = 30000;
                //httpClient.DefaultRequestHeaders.Range = new RangeHeaderValue(0, bytesToRead);
                HttpResponseMessage response = await httpClient.GetAsync(NineGagRandomUrl, HttpCompletionOption.ResponseHeadersRead);
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    //byte[] buffer = new byte[bytesToRead];
                    //stream.Read(buffer, 0, buffer.Length);
                    //string partialHtml = Encoding.UTF8.GetString(buffer);
                    HtmlDocument doc = new HtmlDocument();
                    //doc.LoadHtml(partialHtml);
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