using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Domain.Entities
{
    public class CollectionSubscriber
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
