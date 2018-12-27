namespace Ultrix.Application.DTOs
{
    public class FollowerDto
    {
        public int UserId { get; set; }
        public int FollowerUserId { get; set; }
        public ApplicationUserDto FollowerUser { get; set; }
        public bool IsFollowed { get; set; }
    }
}
