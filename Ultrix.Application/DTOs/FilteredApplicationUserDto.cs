namespace Ultrix.Application.DTOs
{
    public class FilteredApplicationUserDto
    {
        public ApplicationUserDto ApplicationUserDto { get; set; }
        public bool IsFollowed { get; set; }
    }
}
