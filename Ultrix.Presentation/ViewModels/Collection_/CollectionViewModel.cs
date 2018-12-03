using Ultrix.Domain.Entities;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CollectionViewModel
    {
        public Collection Collection { get; }
        public bool IsEditable { get; }

        public CollectionViewModel(Collection collection, bool isEditable)
        {
            Collection = collection;
            IsEditable = isEditable;
        }
    }
}
