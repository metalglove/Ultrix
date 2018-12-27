using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Follower
{
    public class MutualsViewModel
    {
        public IEnumerable<FollowerDto> Followers { get; set; }

        public MutualsViewModel(IEnumerable<FollowerDto> followerDtos)
        {
            Followers = followerDtos;
        }
    }
}
