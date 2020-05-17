using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<List<Category>> GetListAsync()
        {
            return await DbSet.Include(cc => cc.Comics)
                                    .ThenInclude(cc => cc.Comic)
                              .ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await DbSet.Where(c => c.Name.ToLower() == name.ToLower())
                              .FirstOrDefaultAsync();
        }
    }

    public interface ICategoriesRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategoryByNameAsync(string name);
    }
}
