using Podler.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public abstract class AuthorBase : BaseModel, ISelectableItem
    {
        [DataMember]
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Name { get; set; }
    }

    [DataContract]
    public class Author : AuthorBase
    {
        [DataMember]
        public List<ComicAuthor> Comics { get; set; }

        public Author()
        {
            Comics = new List<ComicAuthor>();
        }

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    [DataContract]
    public class AuthorApi : AuthorBase
    {
        [DataMember]
        public IEnumerable<ComicApi> Comics { get; set; }

        public AuthorApi()
        {
            Comics = new List<ComicApi>();
        }
    }
}