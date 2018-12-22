using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeSharingService
    {
        Task<SharedMemeResultDto> ShareMemeToMutualFollowerAsync(SharedMemeDto sharedMemeDto);
        Task<bool> MarkSharedMemeAsSeenAsync(SharedMemeDto sharedMeme);
        Task<IEnumerable<SharedMemeDto>> GetSharedMemeDtos(int userId, SeenStatus seenStatus);
    }
}
