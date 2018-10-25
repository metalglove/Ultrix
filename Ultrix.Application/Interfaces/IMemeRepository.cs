using System.Threading.Tasks;
using Ultrix.Common;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeRepository
    {
        Task<bool> SaveMemeAsync(IMeme meme);
        Task<IMeme> FetchMemeAsync(int id);
    }
}
