using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    internal class MemeLikeConfiguration : IEntityTypeConfiguration<MemeLike>
    {
        public void Configure(EntityTypeBuilder<MemeLike> builder)
        {
            builder.HasKey(memeLike => memeLike.Id);

            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
