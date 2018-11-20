using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    public class SharedMemeConfiguration : IEntityTypeConfiguration<SharedMeme>
    {
        public void Configure(EntityTypeBuilder<SharedMeme> builder)
        {
            builder.HasKey(sharedMeme => sharedMeme.Id);

            //builder.OwnsOne(sharedMeme => sharedMeme.Meme).HasPrincipalKey(sharedMeme => sharedMeme.MemeId);

            builder.Property(p => p.TimestampShared).HasDefaultValueSql("GetDate()");
        }
    }
}
