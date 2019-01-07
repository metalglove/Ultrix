using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class FollowViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please select a user first.")]
        public int UserId { get; set; }

        public FollowerDto GetFollowerDto(int userId)
        {
            return new FollowerDto { FollowerUserId = userId, UserId = UserId };
        }
    }
}
