using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Meme
{
    public class ShareMemeViewModel : AntiForgeryTokenViewModelBase
    {
        public string MemeId { get; set; }
        public int MutualId { get; set; }

        public SharedMemeDto GetSharedMemeDto(int userId)
        {
            return new SharedMemeDto { MemeId = MemeId, ReceiverUserId = MutualId, SenderUserId = userId };
        }
    }
}
