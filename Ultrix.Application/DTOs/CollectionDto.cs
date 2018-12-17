﻿using System.Collections.Generic;

namespace Ultrix.Application.DTOs
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<CollectionItemDetailDto> CollectionItemDetails { get; set; }
        public List<CollectionSubscriberDto> CollectionSubscribers { get; set; }
    }
}
