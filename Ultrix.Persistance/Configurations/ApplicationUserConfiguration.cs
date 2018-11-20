using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities.Authentication;

namespace Ultrix.Persistance.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(user => user.Id);

            builder.HasMany(user => user.Collections)
                .WithOne(collection => collection.User)
                .HasForeignKey(collection => collection.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.CollectionItemDetails)
                .WithOne(collectionItemDetail => collectionItemDetail.User)
                .HasForeignKey(collectionItemDetail => collectionItemDetail.AddedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.CollectionSubscribers)
                .WithOne(collectionSubscriber => collectionSubscriber.User)
                .HasForeignKey(collectionSubscriber => collectionSubscriber.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Comments)
                .WithOne(comment => comment.User)
                .HasForeignKey(comment => comment.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Followers)
                .WithOne(follower => follower.User)
                .HasForeignKey(follower => follower.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Follows)
                .WithOne(follower => follower.FollowerUser)
                .HasForeignKey(follower => follower.FollowerUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.MemeLikes)
                .WithOne(memeLike => memeLike.User)
                .HasForeignKey(memeLike => memeLike.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.ReceivedSharedMemes)
                .WithOne(sharedMeme => sharedMeme.ReceiverUser)
                .HasForeignKey(sharedMeme => sharedMeme.ReceiverUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.SendSharedMemes)
                .WithOne(sharedMeme => sharedMeme.SenderUser)
                .HasForeignKey(sharedMeme => sharedMeme.SenderUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(user => user.UserDetail)
                .WithOne(userDetail => userDetail.ApplicationUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
