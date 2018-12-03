using System;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Domain.Entities
{
    public class SharedMeme
    {
        public int Id { get; set; }
        public int ReceiverUserId { get; set; }
        public virtual ApplicationUser ReceiverUser { get; set; }
        public int SenderUserId { get; set; }
        public virtual ApplicationUser SenderUser { get; set; }
        public string MemeId { get; set; }
        public virtual Meme Meme { get; set; }
        public bool IsSeen { get; set; }
        public DateTime TimestampShared { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && 
                   ReceiverUserId.Equals(((SharedMeme)obj).ReceiverUserId) &&
                   SenderUserId.Equals(((SharedMeme)obj).SenderUserId) &&
                   MemeId.Equals(((SharedMeme)obj).MemeId);
        }
    }
}
