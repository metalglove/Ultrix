using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICollectionItemDetailService
    {
        Task<AddMemeToCollectionResultDto> AddMemeToCollectionAsync(AddMemeToCollectionDto addMemeToCollectionDto);
        Task<bool> RemoveMemeFromCollectionAsync(RemoveMemeFromCollectionDto removeMemeFromCollectionDto);
    }
}
