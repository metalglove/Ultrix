using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Comment
{
    public class CreateCommentViewModel : AntiForgeryTokenViewModelBase
    {
        public string MemeId { get; set; }
        public string Text { get; set; }

        public CommentDto GetCommentDto(int userId)
        {
            return new CommentDto { MemeId = MemeId, Text = Text, UserId = userId };
        }
    }
}
