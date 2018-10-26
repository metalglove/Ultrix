using System.Threading.Tasks;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeService
    {
        Task<Meme> GetRandomMemeAsync();
    }
}