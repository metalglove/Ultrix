using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Meme
{
    public class MemeLikeViewModel : AntiForgeryTokenViewModelBase
    {
        public string MemeId { get; set; }
        public bool IsLike { get; set; }
        // true for actually like or dislike, and false for removing the like or dislike.
        public bool IsLiked { get; set; }

        public MemeLikeDto GetMemeLikeDto(int userId)
        {
            return new MemeLikeDto { MemeId = MemeId, IsLike = IsLike, UserId = userId };
        }
    }
}
