using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Common;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Extensions;

namespace Ultrix.Persistance.Repositories
{
    public class MemeRepository : DbContext, IMemeRepository
    {
        public DbSet<Meme> Memes { get; set; }

        public MemeRepository(DbContextOptions<MemeRepository> options) : base (options)
        {

        }

        public async Task<bool> SaveMemeAsync(IMeme meme)
        {
            await Memes.AddAsync((Meme)meme);
            int saveResult = await SaveChangesAsync();
            return saveResult.Equals(1) ? true : saveResult.Equals(0) ? false : throw new SavingMemeFailedException();
        }
        public async Task<IMeme> FetchMemeAsync(int id)
        {
            Meme fetchedMeme = await Memes.SingleOrDefaultAsync(meme => meme.Id.Equals(id));
            return !fetchedMeme.Equals(default(Meme)) ? fetchedMeme : throw new FetchingMemeFailedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
