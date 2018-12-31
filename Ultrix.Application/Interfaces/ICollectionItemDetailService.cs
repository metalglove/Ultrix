using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionItemDetailService
    {
        Task<ServiceResponseDto> AddMemeToCollectionAsync(AddMemeToCollectionDto addMemeToCollectionDto);
        Task<ServiceResponseDto> RemoveMemeFromCollectionAsync(RemoveMemeFromCollectionDto removeMemeFromCollectionDto);
    }
}
