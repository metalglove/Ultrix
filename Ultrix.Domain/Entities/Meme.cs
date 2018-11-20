using System;
using System.Collections.Generic;

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
        public virtual List<Comment> Comments { get; set; }
        public virtual List<SharedMeme> Shares { get; set; }
        public virtual List<MemeLike> Likes { get; set; }
        public virtual List<CollectionItemDetail> InCollectionItemDetails { get; set; }
    }
}
