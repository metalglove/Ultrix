using System.Threading.Tasks;

namespace Ultrix.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<int> GetUserIdByUserNameAsync(string userName);
    }
}
