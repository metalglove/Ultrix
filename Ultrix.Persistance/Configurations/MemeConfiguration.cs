using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    internal class MemeConfiguration : IEntityTypeConfiguration<Meme>
    {
        public void Configure(EntityTypeBuilder<Meme> builder)
        {
            builder.HasKey(meme => meme.Id);

            builder.HasMany(meme => meme.Comments)
                .WithOne(comment => comment.Meme)
                .HasForeignKey(comment => comment.MemeId);

            builder.HasMany(meme => meme.Shares)
                .WithOne(share => share.Meme)
                .HasForeignKey(share => share.MemeId);

            builder.HasMany(meme => meme.Likes)
                .WithOne(like => like.Meme)
                .HasForeignKey(like => like.MemeId);

            builder.HasMany(meme => meme.InCollectionItemDetails)
                .WithOne(collectionItemDetail => collectionItemDetail.Meme)
                .HasForeignKey(collectionItemDetail => collectionItemDetail.MemeId);

            builder.Property(p => p.TimestampAdded).HasDefaultValueSql("GetDate()");
        }
    }
}
