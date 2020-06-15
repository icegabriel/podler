using Podler.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    public abstract class PublisherBase : BaseModel, ISelectableItem
    {
        [DataMember]
        [Required(ErrorMessage = "O nome da editora é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Name { get; set; }
    } 

    [DataContract]
    public class Publisher : PublisherBase
    {
        [DataMember]
        public List<Comic> Comics { get; set; }

        public Publisher()
        {
            Comics = new List<Comic>();
        }

        public Publisher(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    [DataContract]
    public class PublisherApi : PublisherBase
    {
        [DataMember]
        public IEnumerable<ComicApi> Comics { get; set; }

        public PublisherApi()
        {
            Comics = new List<ComicApi>();
        }
    }
}