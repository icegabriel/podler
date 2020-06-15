using Podler.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public abstract class CategoryBase : BaseModel, ISelectableItem
    {
        [DataMember]
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Name { get; set; }
    }

    [DataContract]
    public class Category : CategoryBase
    {
        [DataMember]
        public List<ComicCategory> Comics { get; set; }

        public Category()
        {
            Comics = new List<ComicCategory>();
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    [DataContract]
    public class CategoryApi : CategoryBase
    {
        [DataMember]
        public List<ComicApi> Comics { get; set; }

        public CategoryApi()
        {
            Comics = new List<ComicApi>();
        }
    }
}