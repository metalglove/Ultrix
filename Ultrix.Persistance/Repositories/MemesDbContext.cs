using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Extensions;

namespace Ultrix.Persistance.Repositories
{
    public class MemesDbContext : DbContext, IMemeRepository
    {
        public DbSet<Meme> Memes { get; set; }

        public MemesDbContext(DbContextOptions options) : base (options)
        {

        }

        public async Task<Meme> FetchMemeAsync(string memeId)
        {
            Meme fetchedMeme = await Memes.SingleOrDefaultAsync(meme => meme.Id.Equals(memeId));
            return !fetchedMeme.Equals(default(Meme)) ? fetchedMeme : throw new FetchingMemeFailedException();
        }
        public async Task<bool> SaveMemeAsync(Meme meme)
        {
            await Memes.AddAsync(meme);
            int saveResult = await SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new SavingMemeFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> DoesMemeExistAsync(Meme meme)
        {
            return await Memes.AnyAsync(memeInDb => memeInDb.Id.Equals(meme.Id));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
