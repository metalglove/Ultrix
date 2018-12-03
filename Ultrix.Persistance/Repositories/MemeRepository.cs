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
        private readonly IEntityValidator<Meme> _memeValidator;

        public MemeRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Meme> memeValidator)
        {
            _applicationDbContext = applicationDbContext;
            _memeValidator = memeValidator;
        }

        public async Task<Meme> GetMemeAsync(string memeId)
        {
            Meme fetchedMeme = await _applicationDbContext.Memes.SingleOrDefaultAsync(meme => meme.Id.Equals(memeId));
            return !fetchedMeme.Equals(default(Meme)) ? fetchedMeme : throw new GettingMemeFailedException();
        }
        public async Task<bool> SaveMemeAsync(Meme meme)
        {
            _memeValidator.Validate(meme);
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
        public async Task<bool> DoesMemeExistAsync(string memeId)
        {
            return await _applicationDbContext.Memes.AnyAsync(memeInDb => memeInDb.Id.Equals(memeId));
        }
    }
}
