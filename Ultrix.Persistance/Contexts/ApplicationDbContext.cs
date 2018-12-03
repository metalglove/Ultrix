using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Extensions;

namespace Ultrix.Persistance.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Meme> Memes { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionItemDetail> CollectionItemDetails { get; set; }
        public DbSet<CollectionSubscriber> CollectionSubscribers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<MemeLike> MemeLikes { get; set; }
        public DbSet<SharedMeme> SharedMemes { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
