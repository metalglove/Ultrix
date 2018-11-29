using Ultrix.Domain.Entities;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CollectionCarouselViewModel
    {
        public Collection Collection { get; private set; }
        public bool IsSubscribable { get; private set; }

        public CollectionCarouselViewModel(Collection collection, bool isSubscribable)
        {
            Collection = collection;
            IsSubscribable = isSubscribable;
        }
    }
}
