using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class UnFollowViewModel : AntiForgeryTokenViewModelBase
    {
        public int FollowerId { get; set; }

        public FollowerDto GetFollowerDto(int userId)
        {
            return new FollowerDto { UserId = FollowerId, FollowerUserId = userId };
        }
    }
}
