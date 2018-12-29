using System;

namespace Ultrix.Application.DTOs
{
    public class CommentDto
    {
        public int UserId { get; set; }
        public ApplicationUserDto User { get; set; }
        public string MemeId { get; set; }
        public string Text { get; set; }
        public DateTime TimestampAdded { get; set; }
    }
}
