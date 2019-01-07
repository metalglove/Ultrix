using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Meme
{
    public class MarkMemeAsSeenViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please select a shared meme.")]
        public int Id { get; set; }

        public SharedMemeDto GetSharedMemeDto(int userId)
        {
            return new SharedMemeDto { Id = Id, ReceiverUserId = userId };
        }
    }
}
