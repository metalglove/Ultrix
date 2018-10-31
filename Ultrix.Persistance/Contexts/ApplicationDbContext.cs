using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Extensions;

namespace Ultrix.Persistance.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Meme> Memes { get; set; }

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
