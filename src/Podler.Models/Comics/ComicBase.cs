using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public abstract class ComicBase : BaseModel
    {
        [DataMember]
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Title { get; set; }

        [DataMember]
        [Required(ErrorMessage = "A descrição e obrigatória")]
        [StringLength(900, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 10)]
        public string Description { get; set; }

        [DataMember]
        [Required(ErrorMessage = "A data de publicação e obrigatória")]
        public DateTime Date { get; set; }

        [DataMember]
        [Required(ErrorMessage = "A editora e obrigatória")]
        public Publisher Publisher { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O status é obrigatório")]
        public ComicStatus Status { get; set; }

        public ComicBase()
        {
            Date = DateTime.Now;
        }
    }

    public enum ComicStatus
    {
        OnGoing = 1,
        Finished = 2,
        Canceled = 3,
        Paused = 4
    }
}
