using System;
using System.Collections.Generic;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Domain.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime TimestampCreated { get; set; }
        public virtual ICollection<Credential> Credentials { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual List<MemeLike> MemeLikes { get; set; }
        public virtual List<Follower> Followers { get; set; }
        public virtual List<Follower> Follows { get; set; }
        public virtual List<Collection> Collections { get; set; }
        public virtual List<CollectionSubscriber> CollectionSubscribers { get; set; }
        public virtual List<CollectionItemDetail> CollectionItemDetails { get; set; }
        public virtual List<SharedMeme> SendSharedMemes { get; set; }
        public virtual List<SharedMeme> ReceivedSharedMemes { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
