using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class UsersViewModel
    {
        public IEnumerable<ApplicationUserDto> Users { get; set; }

        public UsersViewModel(IEnumerable<ApplicationUserDto> users)
        {
            Users = users;
        }
    }
}
