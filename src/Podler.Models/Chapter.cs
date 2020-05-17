using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public class Chapter : BaseModel
    {
        [DataMember]
        [Required(ErrorMessage = "O titulo do captulo é obrigatório")]
        [StringLength(80, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 5)]
        public string Title { get; set; }

        [DataMember]
        public List<Page> Pages { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O numero do captulo é obrigatório")]
        public int CapterNumber { get; set; }

        [DataMember]
        public Comic Comic { get; set; }

        public Chapter()
        {
            Pages = new List<Page>();
        }
    }
}