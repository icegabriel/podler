using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public class AuthorsRepository : BaseRepository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            return await DbSet.Where(c => c.Name.ToLower() == name.ToLower())
                              .FirstOrDefaultAsync();
        }
    }

    public interface IAuthorsRepository : IBaseRepository<Author>
    {
        Task<Author> GetAuthorByNameAsync(string name);
    }
}
