using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultrix.Domain.Entities;

namespace Ultrix.Persistance.Configurations
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.UserName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.TimestampCreated).HasDefaultValueSql("GetDate()");

            builder.HasMany(user => user.Collections)
                .WithOne(collection => collection.User)
                .HasForeignKey(collection => collection.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CollectionItemDetails)
                .WithOne(collectionItemDetail => collectionItemDetail.User)
                .HasForeignKey(collectionItemDetail => collectionItemDetail.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CollectionSubscribers)
                .WithOne(collectionSubscriber => collectionSubscriber.User)
                .HasForeignKey(collectionSubscriber => collectionSubscriber.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.Comments)
                .WithOne(comment => comment.User)
                .HasForeignKey(comment => comment.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.Followers)
                .WithOne(follower => follower.User)
                .HasForeignKey(follower => follower.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.Follows)
                .WithOne(follower => follower.FollowerUser)
                .HasForeignKey(follower => follower.FollowerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.MemeLikes)
                .WithOne(memeLike => memeLike.User)
                .HasForeignKey(memeLike => memeLike.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.ReceivedSharedMemes)
                .WithOne(sharedMeme => sharedMeme.ReceiverUser)
                .HasForeignKey(sharedMeme => sharedMeme.ReceiverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.SendSharedMemes)
                .WithOne(sharedMeme => sharedMeme.SenderUser)
                .HasForeignKey(sharedMeme => sharedMeme.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(user => user.UserDetail)
                .WithOne(userDetail => userDetail.ApplicationUser)
                .HasForeignKey<UserDetail>(user => user.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
