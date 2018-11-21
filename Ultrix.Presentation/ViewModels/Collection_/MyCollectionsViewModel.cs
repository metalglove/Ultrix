using System.Collections.Generic;
using Ultrix.Domain.Entities;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class MyCollectionsViewModel
    {
        public IEnumerable<Collection> MyCollections { get; set; }
        public IEnumerable<Collection> SubscribedCollections { get; set; }

        public MyCollectionsViewModel(IEnumerable<Collection> myCollections, IEnumerable<Collection> subscribedCollections)
        {
            MyCollections = myCollections;
            SubscribedCollections = subscribedCollections;
        }
    }
}
