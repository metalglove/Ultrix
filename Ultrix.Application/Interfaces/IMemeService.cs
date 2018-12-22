using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeService
    {
        Task<MemeDto> GetRandomMemeAsync();
        Task<MemeDto> GetMemeAsync(string memeId);
    }
}
