using System.Threading.Tasks;
using Ultrix.Common;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeRepository
    {
        Task<IMeme> FetchMemeAsync(int id);
        Task<bool> SaveMemeAsync(IMeme meme);
    }
}
