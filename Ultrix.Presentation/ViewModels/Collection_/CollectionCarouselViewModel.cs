using Ultrix.Domain.Entities;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CollectionCarouselViewModel
    {
        public Collection Collection { get; }
        public bool IsSubscribable { get; }

        public CollectionCarouselViewModel(Collection collection, bool isSubscribable)
        {
            Collection = collection;
            IsSubscribable = isSubscribable;
        }
    }
}
