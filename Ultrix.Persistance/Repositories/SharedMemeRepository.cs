using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ultrix.Application.Enumerations;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class SharedMemeRepository : ISharedMemeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<SharedMeme> _sharedMemeValidator;

        public SharedMemeRepository(ApplicationDbContext applicationDbContext,
            IEntityValidator<SharedMeme> sharedMemeValidator)
        {
            _applicationDbContext = applicationDbContext;
            _sharedMemeValidator = sharedMemeValidator;
        }
        public async Task<IEnumerable<SharedMeme>> GetSharedMemesAsync(int userId, SeenStatus seenStatus)
        {
            if (!await _applicationDbContext.SharedMemes.AnyAsync(sharedMeme => sharedMeme.ReceiverUserId.Equals(userId)))
                return default;
            return await _applicationDbContext.SharedMemes
                .Where(sharedMeme => sharedMeme.ReceiverUserId.Equals(userId) &&
                      Convert.ToInt32(sharedMeme.IsSeen).Equals((int)seenStatus) ||
                                     seenStatus.Equals(SeenStatus.Any))
                .ToListAsync();
        }

        public async Task<bool> ShareMemeToUserAsync(SharedMeme sharedMeme)
        {
            _sharedMemeValidator.Validate(sharedMeme);
            if (await _applicationDbContext.SharedMemes.AnyAsync(sharedMemeInDb => sharedMemeInDb.Equals(sharedMeme)))
                return false;
            _applicationDbContext.SharedMemes.Add(sharedMeme);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkSharedMemeAsSeenAsync(SharedMeme sharedMeme)
        {
            if (!await _applicationDbContext.SharedMemes.ContainsAsync(sharedMeme))
                return false;
            sharedMeme.IsSeen = true;
            _applicationDbContext.SharedMemes.Update(sharedMeme);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
