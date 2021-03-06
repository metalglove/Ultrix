﻿using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CollectionSubscriberRepository : RepositoryBase<CollectionSubscriber>
    {
        public CollectionSubscriberRepository(AppDbContext applicationDbContext, IEntityValidator<CollectionSubscriber> collectionSubscriberValidator) : base(applicationDbContext, collectionSubscriberValidator)
        {
        }
    }
}
