using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class AddMemeToCollectionViewModel : AntiForgeryTokenViewModelBase
    {
        public string MemeId { get; set; }
        public int CollectionId { get; set; }

        public AddMemeToCollectionDto GetAddMemeToCollectionDto(int userId)
        {
            return new AddMemeToCollectionDto { CollectionId = CollectionId, UserId = userId, MemeId = MemeId };
        }
    }
}
