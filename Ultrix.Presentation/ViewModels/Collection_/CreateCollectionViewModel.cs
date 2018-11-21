using System.ComponentModel.DataAnnotations;

namespace Ultrix.Presentation.ViewModels.Collection_
{
    public class CreateCollectionViewModel : AntiForgeryTokenViewModelBase
    {
        [Required(ErrorMessage = "Please enter collection name."),
         MinLength(3, ErrorMessage = "The length must be less than {1} characters."),
         MaxLength(250, ErrorMessage = "The length must be less than {1} characters."),
         DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
