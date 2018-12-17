using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CollectionCarouselViewModel
    {
        public CollectionDto Collection { get; }
        public bool IsSubscribable { get; }

        public CollectionCarouselViewModel(CollectionDto collection, bool isSubscribable)
        {
            Collection = collection;
            IsSubscribable = isSubscribable;
        }
    }
}
