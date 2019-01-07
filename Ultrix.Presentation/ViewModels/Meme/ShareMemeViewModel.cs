using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Meme
{
    public class ShareMemeViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please select a meme first."), DataType(DataType.Text), MinLength(6)]
        public string MemeId { get; set; }
        [Required(ErrorMessage = "Please select a mutual friend first.")]
        public int MutualId { get; set; }

        public SharedMemeDto GetSharedMemeDto(int userId)
        {
            return new SharedMemeDto { MemeId = MemeId, ReceiverUserId = MutualId, SenderUserId = userId };
        }
    }
}
