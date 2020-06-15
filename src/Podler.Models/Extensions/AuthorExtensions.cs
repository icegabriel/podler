using System.Collections.Generic;
using System.Linq;

namespace Podler.Models.Extensions
{
    public static class AuthorExtensions
    {
        public static AuthorApi ToAuthorApi(this Author author)
        {
            var comicsApi = author.Comics.Select(c => c.Comic.ToComicApi());

            return new AuthorApi()
            {
                Id = author.Id,
                Name = author.Name,
                Comics = comicsApi
            };
        }

        public static List<AuthorApi> ToListAuthorApi(this List<Author> authors)
        {
            var authorsApi = new List<AuthorApi>();

            foreach (var author in authors)
            {
                authorsApi.Add(author.ToAuthorApi());
            }

            return authorsApi;
        }
    }
}
