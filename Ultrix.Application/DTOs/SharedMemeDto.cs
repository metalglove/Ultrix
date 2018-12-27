using System;

namespace Ultrix.Application.DTOs
{
    public class SharedMemeDto
    {
        public int Id { get; set; }
        public string MemeId { get; set; }
        public MemeDto Meme { get; set; }
        public int SenderUserId { get; set; }
        public ApplicationUserDto SenderUser { get; set; }
        public int ReceiverUserId { get; set; }
        public ApplicationUserDto ReceiverUser { get; set; }
        public bool IsSeen { get; set; }
        public DateTime TimestampShared { get; set; }
    }
}
