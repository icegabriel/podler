using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podler.Models
{
    public class Chapter : BaseModel
    {
        [Required(ErrorMessage = "O titulo do captulo é obrigatório")]
        [StringLength(80, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 5)]
        public string Title { get; set; }

        public List<Page> Pages { get; set; }

        [Required(ErrorMessage = "O numero do captulo é obrigatório")]
        public int CapterNumber { get; set; }

        public Comic Comic { get; set; }
    }
}