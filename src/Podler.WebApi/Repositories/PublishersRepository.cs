using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public class PublishersRepository : BaseRepository<Publisher>, IPublishersRepository
    {
        public PublishersRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Publisher> GetPublisherByNameAsync(string name)
        {
            return await DbSet.Where(c => c.Name.ToLower() == name.ToLower())
                              .FirstOrDefaultAsync();
        }
    }

    public interface IPublishersRepository : IBaseRepository<Publisher>
    {
        Task<Publisher> GetPublisherByNameAsync(string name);
    }
}
