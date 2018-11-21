using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ultrix.Domain.Entities.Authentication
{
    public class ApplicationUser : IdentityUser<int>
    {
        // Id, UserName, PasswordHash in IdentityUser
        public override int Id { get; set; }
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
