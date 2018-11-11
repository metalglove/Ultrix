using System;

namespace Ultrix.Domain.Entities
{
    public class Meme
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string PageUrl { get; set; }
        public DateTime TimestampAdded { get; set; }
    }
}
