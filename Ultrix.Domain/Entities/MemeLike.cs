using System;

namespace Ultrix.Domain.Entities
{
    public class MemeLike
    {
        public int Id { get; set; }
        public string MemeId { get; set; }
        public virtual Meme Meme { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public bool IsLike { get; set; }
        public DateTime TimestampAdded { get; set; }
    }
}
