using System;

namespace Ultrix.Application.DTOs
{
    public class CollectionItemDetailDto
    {
        public int Id { get; set; }
        public string MemeId { get; set; }
        public MemeDto Meme { get; set; }
        public int AddedByUserId { get; set; }
        public int CollectionId { get; set; }
        public DateTime TimestampAdded { get; set; }
    }
}
