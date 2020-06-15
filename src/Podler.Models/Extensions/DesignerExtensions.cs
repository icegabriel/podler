using System.Collections.Generic;
using System.Linq;

namespace Podler.Models.Extensions
{
    public static class DesignerExtensions
    {
        public static DesignerApi ToDesignerApi(this Designer designer)
        {
            var comicsApi = designer.Comics.Select(c => c.Comic.ToComicApi());

            return new DesignerApi()
            {
                Id = designer.Id,
                Name = designer.Name,
                Comics = comicsApi
            };
        }

        public static List<DesignerApi> ToListDesignerApi(this List<Designer> designers)
        {
            var designersApi = new List<DesignerApi>();

            foreach (var designer in designers)
            {
                designersApi.Add(designer.ToDesignerApi());
            }

            return designersApi;
        }
    }
}
