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
            builder.HasKey(memeLike => memeLike.Id);

            //builder.OwnsOne(memeLike => memeLike.Meme).HasPrincipalKey(memeLike => memeLike.MemeId);

            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
