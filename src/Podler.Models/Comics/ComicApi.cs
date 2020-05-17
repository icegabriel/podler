using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Podler.Models
{
    [DataContract]
    public class ComicApi : ComicBase
    {
        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public decimal Score { get; set; }

        [DataMember]
        public int Rank { get; set; }

        [DataMember]
        public int NumberChapters { get; set; }

        [DataMember]
        public List<Designer> Designers { get; set; }

        [DataMember]
        public List<Category> Categories { get; set; }

        [DataMember]
        public List<Author> Authors { get; set; }

        [DataMember]
        public List<Chapter> Chapters { get; set; }

        public ComicApi() : base()
        {
            Designers = new List<Designer>();
            Categories = new List<Category>();
            Authors = new List<Author>();
            Chapters = new List<Chapter>();
        }

        public ComicApi(Comic comic) : this()
        {
            Id = comic.Id;
            Title = comic.Title;
            Description = comic.Description;
            Date = comic.Date;
            Publisher = comic.Publisher;
            Status = comic.Status;
            Score = comic.Score;
            Rank = comic.Rank;
            NumberChapters = comic.NumberChapters;

            Publisher.Comics.Clear();

            comic.Categories.ForEach(cc => {

                cc.Category.Comics.Clear();
                Categories.Add(cc.Category);
            });
            comic.Authors.ForEach(ca => {

                ca.Author.Comics.Clear();
                Authors.Add(ca.Author);
            });
            comic.Designers.ForEach(cd => {

                cd.Designer.Comics.Clear();
                Designers.Add(cd.Designer);
            });

            Cover = $"/api/comics/{Id}/cover";
        }
    }
}
