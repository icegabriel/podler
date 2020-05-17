using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public class Page : BaseModel
    {
        [DataMember]
        [Required]
        public byte[] Image { get; set; }

        public Chapter Chapter { get; set; }
    }

    [DataContract]
    public class Cover : BaseModel
    {
        [DataMember]
        [Required]
        public byte[] Image { get; set; }

        [DataMember]
        public int ComicId { get; set; }
        [DataMember]
        public Comic Comic { get; set; }

        public Cover(byte[] image)
        {
            Image = image;
        }
    }
}