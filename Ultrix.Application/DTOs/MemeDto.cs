using System;

namespace Ultrix.Application.DTOs
{
    public class MemeDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string PageUrl { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is MemeDto that)
            {
                return 
                    this.Id.Equals(that.Id) && 
                    this.Title.Equals(that.Title) &&
                    this.ImageUrl.Equals(that.ImageUrl) &&
                    this.VideoUrl.Equals(that.VideoUrl) &&
                    this.PageUrl.Equals(that.PageUrl);
            }

            return false;
        }
    }
}
