using System;
using System.Collections.Generic;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Domain.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Name { get; set; }
        public DateTime TimestampAdded { get; set; }
        public virtual List<CollectionItemDetail> CollectionItemDetails { get; set; }
        public virtual List<CollectionSubscriber> CollectionSubscribers { get; set; }
    }
}
