using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class DeleteCollectionViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please select a collection first.")]
        public int Id { get; set; }

        public DeleteCollectionDto GetDeleteCollectionDto(int userId)
        {
            return new DeleteCollectionDto { Id = Id, UserId = userId };
        }
    }
}
