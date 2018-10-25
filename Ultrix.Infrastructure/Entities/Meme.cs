using Newtonsoft.Json;
using Ultrix.Common;

namespace Ultrix.Infrastructure.Entities
{
    internal class Meme : IMeme
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("videoUrl")]
        public string VideoUrl { get; set; }
        [JsonProperty("pageUrl")]
        public string PageUrl { get; set; }
    }
}
