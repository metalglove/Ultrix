using System.ComponentModel.DataAnnotations;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CreateCollectionViewModel : AntiForgeryTokenViewModelBase
    {
        private string _collectionName;

        [Required(ErrorMessage = "Please enter collection name."),
         MinLength(3, ErrorMessage = "The length must be at least {1} characters."),
         MaxLength(250, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Text)]
        public string CollectionName
        {
            get => _collectionName.Trim();
            set => _collectionName = value;
        }

        public CollectionDto GetCollectionDto(int userId)
        {
            return new CollectionDto { Name = CollectionName, UserId = userId };
        }
    }
}
