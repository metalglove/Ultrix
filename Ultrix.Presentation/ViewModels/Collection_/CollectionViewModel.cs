using Ultrix.Domain.Entities;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CollectionViewModel
    {
        public Collection Collection { get; private set; }
        public bool IsEditable { get; private set; }

        public CollectionViewModel(Collection collection, bool isEditable)
        {
            Collection = collection;
            IsEditable = isEditable;
        }
    }
}
