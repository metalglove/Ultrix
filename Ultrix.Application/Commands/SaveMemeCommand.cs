using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Utilities;
using Ultrix.Common;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Application.Commands
{
    public class SaveMemeCommand : CommandBase
    {
        private readonly MemesDbContext _memesDbContext;

        public SaveMemeCommand(MemesDbContext memesDbContext)
        {
            _memesDbContext = memesDbContext;
        }

        public async Task<bool> Execute(IMeme meme)
        {
            await _memesDbContext.Memes.AddAsync((Meme)meme);
            int saveResult = await _memesDbContext.SaveChangesAsync();
            return saveResult.Equals(1) ? true : saveResult.Equals(0) ? false : throw new SavingMemeFailedException();
        }
    }
}
