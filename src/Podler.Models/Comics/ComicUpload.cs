using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public class ComicUpload : ComicBase
    {
        [DataMember]
        public byte[] Cover { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O autor é obrigatório")]
        public List<int> SelectedAuthors { get; set; }

        [DataMember]
        [Required(ErrorMessage = "A categoria e obrigatória")]
        public List<int> SelectedCategories { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O desenhista é obrigatório")]
        public List<int> SelectedDesigners { get; set; }

        [DataMember]
        [Required(ErrorMessage = "A editora e obrigatória")]
        public int SelectedPublisher { get; set; }
    }
}
