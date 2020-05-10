using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public class ComicsRepository : BaseRepository<Comic>, IComicsRepository
    {
        public ComicsRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<Comic> GetAsync(int id)
        {
            return await DbSet.Where(c => c.Id == id)
                              .Include(c => c.ComicCategories)
                              .Include(c => c.Publisher)
                              .Include(c => c.ComicAuthors)
                              .Include(c => c.ComicDesigners)
                              .SingleOrDefaultAsync();
        }

        public async Task<byte[]> GetCoverAsync(int id)
        {
            var cover = await _context.Set<Cover>().Where(c => c.ComicId == id).SingleOrDefaultAsync();

            return cover?.Image;
        }
    }

    public interface IComicsRepository : IBaseRepository<Comic>
    {
        Task<byte[]> GetCoverAsync(int id);
    }
}
