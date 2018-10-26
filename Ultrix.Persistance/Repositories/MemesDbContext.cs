using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
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

        //public async Task<Meme> FetchMemeAsync(int id)
        //{
        //    Meme fetchedMeme = await Memes.SingleOrDefaultAsync(meme => meme.Id.Equals(id));
        //    return !fetchedMeme.Equals(default(Meme)) ? fetchedMeme : throw new FetchingMemeFailedException();
        //}
        //public async Task<bool> SaveMemeAsync(Meme meme)
        //{
        //    await Memes.AddAsync(meme);
        //    int saveResult = await SaveChangesAsync();
        //    return saveResult.Equals(1) ? true : saveResult.Equals(0) ? false : throw new SavingMemeFailedException();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
