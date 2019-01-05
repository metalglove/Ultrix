using Microsoft.EntityFrameworkCore;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Domain.Enumerations;
using Ultrix.Persistance.Extensions;

namespace Ultrix.Persistance.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<CredentialType> CredentialTypes { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Meme> Memes { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionItemDetail> CollectionItemDetails { get; set; }
        public DbSet<CollectionSubscriber> CollectionSubscribers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<MemeLike> MemeLikes { get; set; }
        public DbSet<SharedMeme> SharedMemes { get; set; }

        public AppDbContext(DbContextOptions options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
