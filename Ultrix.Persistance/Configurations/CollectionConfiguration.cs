using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.HasKey(collection => collection.Id);

            builder.HasMany(collection => collection.CollectionItemDetails)
                .WithOne(collectionItemDetail => collectionItemDetail.Collection)
                .HasForeignKey(collectionItemDetail => collectionItemDetail.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(collection => collection.CollectionSubscribers)
                .WithOne(collectionSubscriber => collectionSubscriber.Collection)
                .HasForeignKey(collectionSubscriber => collectionSubscriber.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
