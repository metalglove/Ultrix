using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class MemeLikeConfiguration : IEntityTypeConfiguration<MemeLike>
    {
        public void Configure(EntityTypeBuilder<MemeLike> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
