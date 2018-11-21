using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeRepository
    {
        Task<Meme> GetMemeAsync(string memeId);
        Task<bool> SaveMemeAsync(Meme meme);
        Task<bool> SaveMemesAsync(IEnumerable<Meme> memes);
        Task<bool> DoesMemeExistAsync(Meme meme);
    }
}
