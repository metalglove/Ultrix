using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class UsersViewModel
    {
        public IEnumerable<FilteredApplicationUserDto> Users { get; set; }

        public UsersViewModel(IEnumerable<FilteredApplicationUserDto> users)
        {
            Users = users;
        }
    }
}
