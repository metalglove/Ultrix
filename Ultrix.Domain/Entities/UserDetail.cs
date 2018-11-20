using System;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Domain.Entities
{
    public class UserDetail
    {
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ProfilePictureData { get; set; }
        public DateTime TimestampCreated { get; set; }
    }
}
