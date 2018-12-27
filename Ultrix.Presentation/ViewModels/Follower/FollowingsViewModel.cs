using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class FollowingsViewModel
    {
        public IEnumerable<FollowerDto> Followings { get; set; }

        public FollowingsViewModel(IEnumerable<FollowerDto> followerDtos)
        {
            Followings = followerDtos;
        }
    }
}
