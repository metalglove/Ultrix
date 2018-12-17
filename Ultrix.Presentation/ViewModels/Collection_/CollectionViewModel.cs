using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CollectionViewModel
    {
        public CollectionDto Collection { get; }
        public bool IsEditable { get; }

        public CollectionViewModel(CollectionDto collection, bool isEditable)
        {
            Collection = collection;
            IsEditable = isEditable;
        }
    }
}
