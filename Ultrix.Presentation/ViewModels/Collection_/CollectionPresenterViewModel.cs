using Ultrix.Domain.Entities;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CollectionPresenterViewModel
    {
        public Collection Collection { get; private set; }
        public bool IsSubscribable { get; private set; }

        public CollectionPresenterViewModel(Collection collection, bool isSubscribable)
        {
            Collection = collection;
            IsSubscribable = isSubscribable;
        }
    }
}
