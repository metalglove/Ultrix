using Microsoft.EntityFrameworkCore;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Extensions;

namespace Ultrix.Persistance.Repositories
{
    public class MemesDbContext : DbContext
    {
        public DbSet<Meme> Memes { get; set; }

        public MemesDbContext(DbContextOptions<MemesDbContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
