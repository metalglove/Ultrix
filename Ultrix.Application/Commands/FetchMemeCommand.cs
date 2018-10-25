using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Utilities;
using Ultrix.Common;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Application.Commands
{
    public class FetchMemeCommand : CommandBase
    {
        private readonly MemesDbContext _memesDbContext;

        public FetchMemeCommand(MemesDbContext memesDbContext)
        {
            _memesDbContext = memesDbContext;
        }

        public async Task<IMeme> Execute(int id)
        {
            Meme fetchedMeme = await _memesDbContext.Memes.SingleOrDefaultAsync(meme => meme.Id.Equals(id));
            return !fetchedMeme.Equals(default(Meme)) ? fetchedMeme : throw new FetchingMemeFailedException();
        }
    }
}
