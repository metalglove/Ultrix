using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class CollectionItemDetailConfiguration : IEntityTypeConfiguration<CollectionItemDetail>
    {
        public void Configure(EntityTypeBuilder<CollectionItemDetail> builder)
        {
            builder.HasKey(collectionItemDetail => collectionItemDetail.Id);

            //builder.OwnsOne(collectionItemDetail => collectionItemDetail.Meme).HasPrincipalKey(collectionItemDetail => collectionItemDetail.MemeId);

            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
