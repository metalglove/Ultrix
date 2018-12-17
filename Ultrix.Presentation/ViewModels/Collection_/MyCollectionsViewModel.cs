using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class MyCollectionsViewModel
    {
        public IEnumerable<CollectionDto> MyCollections { get; set; }
        public IEnumerable<CollectionDto> SubscribedCollections { get; set; }

        public MyCollectionsViewModel(IEnumerable<CollectionDto> myCollections, IEnumerable<CollectionDto> subscribedCollections)
        {
            MyCollections = myCollections;
            SubscribedCollections = subscribedCollections;
        }
    }
}
