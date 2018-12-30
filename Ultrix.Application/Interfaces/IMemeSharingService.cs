using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Domain.Enumerations;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeSharingService
    {
        Task<ServiceResponseDto> ShareMemeToMutualFollowerAsync(SharedMemeDto sharedMemeDto);
        Task<SharedMemeMarkAsSeenDto> MarkSharedMemeAsSeenAsync(SharedMemeDto sharedMeme);
        Task<IEnumerable<SharedMemeDto>> GetSharedMemesAsync(int userId, SeenStatus seenStatus);
    }
}
