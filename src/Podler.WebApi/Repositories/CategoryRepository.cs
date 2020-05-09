using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }

        public async override Task<Category> GetAsync(int id)
        {
            return await DbSet.Where(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await DbSet.Where(c => c.Name.ToLower() == name.ToLower())
                              .FirstOrDefaultAsync();
        }
    }

    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategoryByNameAsync(string name);
    }
}
