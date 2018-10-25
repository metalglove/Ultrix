using System.Threading.Tasks;
using Ultrix.Common;

namespace Ultrix.Application.Interfaces
{
    public interface IMemeService
    {
        Task<IMeme> GetRandomMemeAsync();
    }
}