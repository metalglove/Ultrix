using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> GetUserIdByUserNameAsync(string userName)
        {
            ApplicationUser usr = await _applicationDbContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(userName));
            if (usr == default)
                throw new ApplicationUserNotFoundException();
            return usr.Id;
        }
        public async Task<string> GetUserNameByUserIdAsync(int userId)
        {
            ApplicationUser usr = await _applicationDbContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
            if (usr == default)
                throw new ApplicationUserNotFoundException();
            return usr.UserName;
        }
    }
}
