using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Comment
{
    public class CommentsViewModel
    {
        public string MemeId { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }

        public CommentsViewModel(IEnumerable<CommentDto> comments, string memeId)
        {
            Comments = comments;
            MemeId = memeId;
        }
    }
}
