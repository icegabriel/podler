namespace Podler.Models
{
    public class Page : BaseModel
    {
        public byte[] Image { get; set; }

        public Chapter Chapter { get; set; }
    }

    public class Cover : BaseModel
    {
        public byte[] Image { get; set; }

        public int ComicId { get; set; }
        public Comic Comic { get; set; }
    }
}