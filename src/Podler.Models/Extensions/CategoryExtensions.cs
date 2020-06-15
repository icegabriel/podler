using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podler.Models.Extensions
{
    public static class CategoryExtensions
    {
        public static CategoryApi ToCategoryApi(this Category category)
        {
            var comicsApi = category.Comics.ToList().Select(c => c.Comic.ToComicApi());

            return new CategoryApi()
            {
                Id = category.Id,
                Name = category.Name,
                Comics = comicsApi.ToList()
            };
        }

        public static List<CategoryApi> ToListCategoryApi(this List<Category> categories)
        {
            var categoriesApi = new List<CategoryApi>();

            foreach (var category in categories)
            {
                categoriesApi.Add(category.ToCategoryApi());
            }

            return categoriesApi;
        }
    }
}
