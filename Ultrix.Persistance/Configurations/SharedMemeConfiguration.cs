using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    internal class SharedMemeConfiguration : IEntityTypeConfiguration<SharedMeme>
    {
        public void Configure(EntityTypeBuilder<SharedMeme> builder)
        {
            builder.HasKey(sharedMeme => sharedMeme.Id);

            builder.Property(p => p.TimestampShared).HasDefaultValueSql("GetDate()");
        }
    }
}
