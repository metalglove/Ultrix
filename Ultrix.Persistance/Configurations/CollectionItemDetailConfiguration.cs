using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    internal class CollectionItemDetailConfiguration : IEntityTypeConfiguration<CollectionItemDetail>
    {
        public void Configure(EntityTypeBuilder<CollectionItemDetail> builder)
        {
            builder.HasKey(collectionItemDetail => collectionItemDetail.Id);

            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
