using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Comment
{
    public class CreateCommentViewModel : AntiForgeryTokenViewModelBase
    {
        private string _text;

        [Required, DataType(DataType.Text), MinLength(6)]
        public string MemeId { get; set; }

        [Required, DataType(DataType.Text), MinLength(10, ErrorMessage = "Please enter a comment of at least 10 characters.")]
        public string Text
        {
            get => _text.Trim();
            set => _text = value;
        }

        public CommentDto GetCommentDto(int userId)
        {
            return new CommentDto { MemeId = MemeId, Text = Text, UserId = userId };
        }
    }
}
