using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class CollectionSubscriberConfiguration : IEntityTypeConfiguration<CollectionSubscriber>
    {
        public void Configure(EntityTypeBuilder<CollectionSubscriber> builder)
        {
            builder.HasKey(collectionSubscriber => collectionSubscriber.Id);
        }
    }
}
