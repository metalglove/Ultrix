using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class MemeRepository : IMemeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MemeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Meme> GetMemeAsync(string memeId)
        {
            Meme fetchedMeme = await _applicationDbContext.Memes.SingleOrDefaultAsync(meme => meme.Id.Equals(memeId));
            return !fetchedMeme.Equals(default(Meme)) ? fetchedMeme : throw new GettingMemeFailedException();
        }
        public async Task<bool> SaveMemeAsync(Meme meme)
        {
            await _applicationDbContext.Memes.AddAsync(meme);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
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
        public async Task<bool> SaveMemesAsync(IEnumerable<Meme> memes)
        {
            await _applicationDbContext.Memes.AddRangeAsync(memes);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
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
            return await _applicationDbContext.Memes.AnyAsync(memeInDb => memeInDb.Id.Equals(meme.Id));
        }
    }
}
