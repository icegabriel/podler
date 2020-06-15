using System.Collections.Generic;
using System.Linq;

namespace Podler.Models.Extensions
{
    public static class PublisherExtensions
    {
        public static PublisherApi ToPublisherApi(this Publisher publisher)
        {
            var comicsApi = publisher.Comics.Select(c => c.ToComicApi());

            return new PublisherApi()
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Comics = comicsApi
            };
        }

        public static List<PublisherApi> ToListPublisherApi(this List<Publisher> publishers)
        {
            var publishersApi = new List<PublisherApi>();

            foreach (var publisher in publishers)
            {
                publishersApi.Add(publisher.ToPublisherApi());
            }

            return publishersApi;
        }
    }
}
