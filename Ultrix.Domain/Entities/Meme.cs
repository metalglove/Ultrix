using Ultrix.Common;

namespace Ultrix.Domain.Entities
{
    public class Meme : IMeme
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string PageUrl { get; set; }
    }
}
