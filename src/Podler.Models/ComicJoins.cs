using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public class ComicCategory
    {
        [DataMember]
        public int ComicId { get; set; }
        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public Comic Comic { get; set; }
        [DataMember]
        public Category Category { get; set; }
    }

    [DataContract]
    public class ComicAuthor
    {
        [DataMember]
        public int ComicId { get; set; }
        [DataMember]
        public int AuthorId { get; set; }

        [DataMember]
        public Comic Comic { get; set; }
        [DataMember]
        public Author Author { get; set; }
    }

    [DataContract]
    public class ComicDesigner
    {
        [DataMember]
        public int ComicId { get; set; }
        [DataMember]
        public int DesignerId { get; set; }

        [DataMember]
        public Comic Comic { get; set; }
        [DataMember]
        public Designer Designer { get; set; }
    }
}
