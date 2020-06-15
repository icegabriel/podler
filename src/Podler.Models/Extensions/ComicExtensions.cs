using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podler.Models.Extensions
{
    public static class ComicExtensions
    {
        public static ComicApi ToComicApi(this Comic comic)
        {
            var authors = comic.Authors.Select(ca => new ComicAuthor()
            {
                ComicId = ca.ComicId,
                AuthorId = ca.AuthorId,
                Author = new Author(ca.Author.Id, ca.Author.Name)
            }).ToList();

            var designers = comic.Designers.Select(cd => new ComicDesigner()
            {
                ComicId = cd.ComicId,
                DesignerId = cd.DesignerId,
                Designer = new Designer(cd.Designer.Id, cd.Designer.Name)
            }).ToList();

            var categories = comic.Categories.Select(cc => new ComicCategory()
            {
                ComicId = cc.ComicId,
                CategoryId = cc.CategoryId,
                Category = new Category(cc.Category.Id, cc.Category.Name)
            }).ToList();

            return new ComicApi
            {
                Id = comic.Id,
                Title = comic.Title,
                Description = comic.Description,
                Date = comic.Date,
                Publisher = new Publisher(comic.Publisher.Id, comic.Publisher.Name),
                Status = comic.Status,
                Score = comic.Score,
                Rank = comic.Rank,
                NumberChapters = comic.NumberChapters,
                Cover = $"/api/comics/{comic.Id}/cover",
                Designers = designers,
                Authors = authors,
                Categories = categories
            };
        }

        public static List<ComicApi> ToListComicApi(this List<Comic> comics)
        {
            var comicsApi = new List<ComicApi>();

            foreach (var comic in comics)
                comicsApi.Add(comic.ToComicApi());

            return comicsApi;
        }
    }
}
