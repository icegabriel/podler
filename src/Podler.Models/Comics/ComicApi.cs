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
        public List<ComicDesigner> Designers { get; set; }

        [DataMember]
        public List<ComicCategory> Categories { get; set; }

        [DataMember]
        public List<ComicAuthor> Authors { get; set; }

        [DataMember]
        public Publisher Publisher { get; set; }

        [DataMember]
        public List<Chapter> Chapters { get; set; }

        public ComicApi() : base()
        {
            Designers = new List<ComicDesigner>();
            Categories = new List<ComicCategory>();
            Authors = new List<ComicAuthor>();
            Chapters = new List<Chapter>();
        }
    }
}
