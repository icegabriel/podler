using System.ComponentModel.DataAnnotations;

namespace Podler.Models
{
    public class ComicCategory
    {
        public int ComicId { get; set; }
        public int CategoryId { get; set; }

        public Comic Comic { get; set; }
        public Category Category { get; set; }
    }

    public class ComicAuthor
    {
        public int ComicId { get; set; }
        public int AuthorId { get; set; }

        public Comic Comic { get; set; }
        public Author Author { get; set; }
    }

    public class ComicDesigner
    {
        public int ComicId { get; set; }
        public int DesignerId { get; set; }

        public Comic Comic { get; set; }
        public Designer Designer { get; set; }
    }
}
