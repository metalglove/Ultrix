using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        public async Task<bool> LikeMemeAsync(MemeLike memeLike)
        {
            Expression<Func<MemeLike, bool>> predicate = ml => ml.MemeId.Equals(memeLike.MemeId) && ml.UserId.Equals(memeLike.UserId);
            if (await _applicationDbContext.MemeLikes.AnyAsync(predicate))
            {
                MemeLike ExistingMemeLike = await _applicationDbContext.MemeLikes.FirstAsync(predicate);
                if (ExistingMemeLike.IsLike)
                    return true;
                else
                    ExistingMemeLike.IsLike = false;
                _applicationDbContext.MemeLikes.Update(ExistingMemeLike);
            }
            else
            {
                await _applicationDbContext.MemeLikes.AddAsync(memeLike);
            }
            
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new LikingMemeFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> UnLikeMemeAsync(string memeId, int userId)
        {
            MemeLike memeLike = await _applicationDbContext.MemeLikes.FirstOrDefaultAsync(ml => ml.MemeId.Equals(memeId) && ml.UserId.Equals(userId));
            if (memeLike == default)
                throw new MemeLikeNotFoundException();

            _applicationDbContext.MemeLikes.Remove(memeLike);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new UnLikingMemeFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> DisLikeMemeAsync(MemeLike memeLike)
        {
            Expression<Func<MemeLike, bool>> predicate = ml => ml.MemeId.Equals(memeLike.MemeId) && ml.UserId.Equals(memeLike.UserId);
            if (await _applicationDbContext.MemeLikes.AnyAsync(predicate))
            {
                MemeLike ExistingMemeLike = await _applicationDbContext.MemeLikes.FirstAsync(predicate);
                if (!ExistingMemeLike.IsLike)
                    return true;
                else
                    ExistingMemeLike.IsLike = true;
                _applicationDbContext.MemeLikes.Update(ExistingMemeLike);
            }
            else
            {
                await _applicationDbContext.MemeLikes.AddAsync(memeLike);
            }
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DislikingMemeFailedException();
            }
            return saveSuccess;
        }
        public async Task<bool> UnDisLikeMemeAsync(string memeId, int userId)
        {
            MemeLike memeLike = await _applicationDbContext.MemeLikes.FirstOrDefaultAsync(ml => ml.MemeId.Equals(memeId) && ml.UserId.Equals(userId));
            if (memeLike == default)
                throw new MemeLikeNotFoundException();

            _applicationDbContext.MemeLikes.Remove(memeLike);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new UnDislikingMemeFailedException();
            }
            return saveSuccess;
        }
    }
}
