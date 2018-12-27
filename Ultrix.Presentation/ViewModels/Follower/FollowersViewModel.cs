using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class FollowersViewModel
    {
        public IEnumerable<FollowerDto> Followers { get; set; }

        public FollowersViewModel(IEnumerable<FollowerDto> followerDtos)
        {
            Followers = followerDtos;
        }
    }
}
