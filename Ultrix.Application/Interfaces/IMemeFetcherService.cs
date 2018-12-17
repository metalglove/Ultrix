using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeFetcherService
    {
        Task<MemeDto> GetRandomMemeAsync();
    }
}