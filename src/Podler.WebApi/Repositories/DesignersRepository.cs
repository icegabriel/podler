using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public class DesignersRepository : BaseRepository<Designer>, IDesignersRepository
    {
        public DesignersRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Designer> GetDesignerByNameAsync(string name)
        {
            return await DbSet.Where(c => c.Name.ToLower() == name.ToLower())
                              .FirstOrDefaultAsync();
        }
    }

    public interface IDesignersRepository : IBaseRepository<Designer>
    {
        Task<Designer> GetDesignerByNameAsync(string name);
    }
}
