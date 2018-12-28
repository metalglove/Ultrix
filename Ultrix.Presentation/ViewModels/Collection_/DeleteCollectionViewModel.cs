using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class DeleteCollectionViewModel : AntiForgeryTokenViewModelBase
    {
        public int Id { get; set; }

        public DeleteCollectionDto GetDeleteCollectionDto(int userId)
        {
            return new DeleteCollectionDto { Id = Id, UserId = userId };
        }
    }
}
