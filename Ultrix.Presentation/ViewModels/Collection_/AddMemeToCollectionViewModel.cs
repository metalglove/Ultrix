using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class AddMemeToCollectionViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please select a meme first."), DataType(DataType.Text), MinLength(6)]
        public string MemeId { get; set; }
        [Required(ErrorMessage = "Please select a collection first.")]
        public int CollectionId { get; set; }

        public AddMemeToCollectionDto GetAddMemeToCollectionDto(int userId)
        {
            return new AddMemeToCollectionDto { CollectionId = CollectionId, UserId = userId, MemeId = MemeId };
        }
    }
}
