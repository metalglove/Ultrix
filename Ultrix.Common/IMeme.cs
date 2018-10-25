namespace Ultrix.Common
{
    public interface IMeme
    {
        string Id { get; }
        string Title { get; }
        string ImageUrl { get; }
        string VideoUrl { get; }
        string PageUrl { get; }
    }
}
