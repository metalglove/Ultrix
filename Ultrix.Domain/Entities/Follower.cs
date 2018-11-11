using System;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Domain.Entities
{
    public class Follower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }
        public int FollowerUserId { get; set; }
        //public virtual ApplicationUser FollowerUser { get; set; }
        public DateTime TimestampFollowed { get; set; }
    }
}
