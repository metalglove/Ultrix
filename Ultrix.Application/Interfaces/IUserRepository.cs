using System.Threading.Tasks;

namespace Ultrix.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<string> GetUserNameByUserIdAsync(int userId);
        Task<int> GetUserIdByUserNameAsync(string userName);
    }
}
