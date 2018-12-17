namespace Ultrix.Application.DTOs
{
    public class CollectionSubscriberDto
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int UserId { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
