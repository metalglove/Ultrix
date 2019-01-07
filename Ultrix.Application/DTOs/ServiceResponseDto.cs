namespace Ultrix.Application.DTOs
{
    public class ServiceResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }
    }
}
