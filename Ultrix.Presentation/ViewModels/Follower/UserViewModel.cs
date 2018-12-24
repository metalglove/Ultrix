using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class UserViewModel
    {
        public ApplicationUserDto ApplicationUserDto { get; set; }
        public bool IsFollowed { get; set; }

        public UserViewModel(ApplicationUserDto applicationUserDto, bool isFollowed)
        {
            ApplicationUserDto = applicationUserDto;
            IsFollowed = isFollowed;
        }
    }
}
