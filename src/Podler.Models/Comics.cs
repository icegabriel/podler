using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

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
        public List<ComicDesigner> ComicDesigners { get; set; }

        [DataMember]
        public List<ComicCategory> ComicCategories { get; set; }

        [DataMember]
        public List<ComicAuthor> ComicAuthors { get; set; }

        [DataMember]
        [Required(ErrorMessage = "A editora e obrigatória")]
        public Publisher Publisher { get; set; }

        [DataMember]
        [Required(ErrorMessage = "A data de publicação e obrigatória")]
        public DateTime Date { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O status é obrigatório")]
        public ComicStatus Status { get; set; }

        public ComicBase()
        {
            ComicCategories = new List<ComicCategory>();
            ComicAuthors = new List<ComicAuthor>();
            ComicDesigners = new List<ComicDesigner>();
        }

        public ComicBase(string title, string description, Publisher publisher, DateTime date) : this()
        {
            Title = title;
            Description = description;
            Date = date;
            Publisher = publisher;
        }
    }

    [DataContract]
    public class Comic : ComicBase
    {
        [DataMember]
        //[Required(ErrorMessage = "A imagem de capa e obrigatória")]
        public Cover Cover { get; set; }

        [DataMember]
        public decimal Score { get; set; }

        [DataMember]
        public int Rank { get; set; }

        [DataMember]
        public int NumberChapters { get; set; }

        [DataMember]
        [Required]
        public List<Chapter> Chapters { get; set; }

        public Comic() : base()
        {
            Chapters = new List<Chapter>();
        }
    }

    public class ComicApi : ComicBase
    {
        [Required(ErrorMessage = "A imagem de capa e obrigatória")]
        public string Cover { get; set; }

        public decimal Score { get; set; }

        public int Rank { get; set; }

        public int NumberChapters { get; set; }

        [Required]
        public List<Chapter> Chapters { get; set; }

        public ComicApi() : base()
        {
            Chapters = new List<Chapter>();
        }

        public ComicApi(Comic comic)
        {
            Id = comic.Id;
            Title = comic.Title;
            Description = comic.Description;
            ComicCategories = comic.ComicCategories;
            Publisher = comic.Publisher;
            Date = comic.Date;
            Status = comic.Status;
            ComicAuthors = comic.ComicAuthors;
            ComicDesigners = comic.ComicDesigners;
            Score = comic.Score;
            Rank = comic.Rank;
            NumberChapters = comic.NumberChapters;
            Chapters = comic.Chapters;
            Cover = $"/api/comics/{Id}/cover";
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
