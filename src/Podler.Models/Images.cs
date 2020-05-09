namespace Podler.Models
{
    public class PageImage : BaseModel
    {
        public byte[] Image { get; set; }

        public Chapter Chapter { get; set; }
    }

    public class CoverImage : BaseModel
    {
        public byte[] Image { get; set; }

        public int ComicId { get; set; }
        public Comic Comic { get; set; }
    }
}