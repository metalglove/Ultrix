using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class CollectionItemDetailConfiguration : IEntityTypeConfiguration<CollectionItemDetail>
    {
        public void Configure(EntityTypeBuilder<CollectionItemDetail> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
