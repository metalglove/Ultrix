namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class AddMemeToCollectionViewModel : AntiForgeryTokenViewModelBase
    {
        public string MemeId { get; set; }
        public int CollectionId { get; set; }
    }
}
