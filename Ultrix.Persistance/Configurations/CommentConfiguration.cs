using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
