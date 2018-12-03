using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.Interfaces
{
    public interface ISharedMemeRepository
    {
        Task<IEnumerable<SharedMeme>> GetSharedMemesAsync(int userId, SeenStatus seenStatus);
        Task<bool> ShareMemeToUserAsync(SharedMeme sharedMeme);
        Task<bool> MarkSharedMemeAsSeenAsync(SharedMeme sharedMeme);
    }
}
