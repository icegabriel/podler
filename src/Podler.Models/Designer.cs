using Podler.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public abstract class DesignerBase : BaseModel, ISelectableItem
    {
        [DataMember]
        [Required(ErrorMessage = "O nome do desenhista é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Name { get; set; }
    }

    [DataContract]
    public class Designer : DesignerBase
    {
        [DataMember]
        public List<ComicDesigner> Comics { get; set; }

        public Designer()
        {
            Comics = new List<ComicDesigner>();
        }

        public Designer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    [DataContract]
    public class DesignerApi : DesignerBase
    {
        [DataMember]
        public IEnumerable<ComicApi> Comics { get; set; }

        public DesignerApi()
        {
            Comics = new List<ComicApi>();
        }
    }
}