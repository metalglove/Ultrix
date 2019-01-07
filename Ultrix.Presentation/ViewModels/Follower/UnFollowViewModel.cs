using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class UnFollowViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please select a follower first.")]
        public int FollowerId { get; set; }

        public FollowerDto GetFollowerDto(int userId)
        {
            return new FollowerDto { UserId = FollowerId, FollowerUserId = userId };
        }
    }
}
