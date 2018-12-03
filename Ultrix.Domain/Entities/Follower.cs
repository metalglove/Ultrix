using System;

namespace Ultrix.Domain.Entities
{
    public class Follower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int FollowerUserId { get; set; }
        public virtual ApplicationUser FollowerUser { get; set; }
        public DateTime TimestampFollowed { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && 
                   UserId.Equals(((Follower)obj).UserId) && 
                   FollowerUserId.Equals(((Follower)obj).FollowerUserId);
        }
    }
}
