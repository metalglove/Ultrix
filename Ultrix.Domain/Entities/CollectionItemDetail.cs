using System;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Domain.Entities
{
    public class CollectionItemDetail
    {
        public int Id { get; set; }
        public int AddedByUserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; }
        public string MemeId { get; set; }
        public virtual Meme Meme { get; set; }
        public DateTime TimestampAdded { get; set; }
    }
}