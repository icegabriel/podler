using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public class Comic : ComicBase
    {
        [DataMember]
        [Required(ErrorMessage = "A imagem de capa e obrigatória")]
        public Cover Cover { get; set; }

        [DataMember]
        public decimal Score { get; set; }

        [DataMember]
        public int Rank { get; set; }

        [DataMember]
        public int NumberChapters { get; set; }

        [DataMember]
        public List<ComicDesigner> Designers { get; set; }

        [DataMember]
        public List<ComicCategory> Categories { get; set; }

        [DataMember]
        public List<ComicAuthor> Authors { get; set; }

        [DataMember]
        public Publisher Publisher { get; set; }

        [DataMember]
        public List<Chapter> Chapters { get; set; }

        public Comic() : base()
        {
            Designers = new List<ComicDesigner>();
            Categories = new List<ComicCategory>();
            Authors = new List<ComicAuthor>();
            Chapters = new List<Chapter>();
            Publisher = new Publisher();
        }

        public Comic(ComicUpload comicUpload): this()
        {
            Title = comicUpload.Title;
            Description = comicUpload.Description;
            Date = comicUpload.Date;
            Status = comicUpload.Status;
            Cover = new Cover(comicUpload.Cover);
        }

        public void IncludeCategory(Category category) =>
            Categories.Add(new ComicCategory() { Category = category });

        public void IncludeAuthor(Author author) =>
            Authors.Add(new ComicAuthor() { Author = author });

        public void IncludeDesigner(Designer designer) =>
            Designers.Add(new ComicDesigner() { Designer = designer });
    }
}
