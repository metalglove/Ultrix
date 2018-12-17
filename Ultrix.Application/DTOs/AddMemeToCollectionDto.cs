namespace Ultrix.Application.DTOs
{
    public class AddMemeToCollectionDto
    {
        public string MemeId { get; set; }
        public int CollectionId { get; set; }
        public int UserId { get; set; }
    }
}
