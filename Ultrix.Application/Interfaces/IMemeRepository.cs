using System.Threading.Tasks;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeRepository
    {
        Task<Meme> FetchMemeAsync(string memeId);
        Task<bool> SaveMemeAsync(Meme meme);
        Task<bool> DoesMemeExistAsync(Meme meme);
    }
}
