using Podler.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podler.Models
{
    public class Designer : BaseModel, ISelectListItem
    {
        [Required(ErrorMessage = "O nome do desenhista é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Name { get; set; }

        public List<ComicDesigner> ComicDesigners { get; set; }

        public Designer()
        {
            ComicDesigners = new List<ComicDesigner>();
        }
    }
}