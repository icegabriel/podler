using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public class ComicsRepository : BaseRepository<Comic>, IComicsRepository
    {
        private readonly IPublishersRepository _publishersRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IDesignersRepository _designersRepository;

        public ComicsRepository(ApplicationContext context,
                                IPublishersRepository publishersRepository,
                                ICategoriesRepository categoriesRepository,
                                IAuthorsRepository authorsRepository,
                                IDesignersRepository designersRepository) : base(context)
        {
            _publishersRepository = publishersRepository;
            _categoriesRepository = categoriesRepository;
            _authorsRepository = authorsRepository;
            _designersRepository = designersRepository;
        }

        public override async Task<Comic> GetAsync(int id)
        {
            return await DbSet.Where(c => c.Id == id)
                              .Include(c => c.Categories)
                                .ThenInclude(c => c.Category)
                              .Include(c => c.Publisher)
                              .Include(c => c.Authors)
                                .ThenInclude(a => a.Author)
                              .Include(c => c.Designers)
                                .ThenInclude(d => d.Designer)
                              .SingleOrDefaultAsync();
        }

        public override async Task<List<Comic>> GetListAsync()
        {
            return await DbSet.Include(c => c.Categories)
                                    .ThenInclude(c => c.Category)
                                .Include(c => c.Publisher)
                                .Include(c => c.Authors)
                                    .ThenInclude(a => a.Author)
                                .Include(c => c.Designers)
                                    .ThenInclude(d => d.Designer)
                                .ToListAsync();
        }

        public async Task<Comic> GetComicByTitleAsync(string title)
        {
            return await DbSet.Where(c => c.Title.ToLower() == title.ToLower())
                              .FirstOrDefaultAsync();
        }

        public async Task<byte[]> GetCoverAsync(int id)
        {
            var cover = await _context.Set<Cover>().Where(c => c.ComicId == id).SingleOrDefaultAsync();

            return cover?.Image;
        }

        public async Task<Comic> IncludeComicUploadAsync(ComicUpload comicUpload)
        {
            var comic = await ToComic(comicUpload);

            await IncludeAsync(comic);

            return comic;
        }

        public async Task<Comic> ToComic(ComicUpload comicUpload)
        {
            var comic = new Comic(comicUpload);

            comic.Publisher = await _publishersRepository.GetAsync(comicUpload.SelectedPublisher);

            comicUpload.SelectedCategories.ForEach(async ci =>
            {
                var category = await _categoriesRepository.GetAsync(ci);
                comic.IncludeCategory(category);
            });

            comicUpload.SelectedAuthors.ForEach(async ai =>
            {
                var author = await _authorsRepository.GetAsync(ai);
                comic.IncludeAuthor(author);
            });

            comicUpload.SelectedDesigners.ForEach(async di =>
            {
                var designer = await _designersRepository.GetAsync(di);
                comic.IncludeDesigner(designer);
            });

            return comic;
        }
    }

    public interface IComicsRepository : IBaseRepository<Comic>
    {
        Task<byte[]> GetCoverAsync(int id);
        Task<Comic> IncludeComicUploadAsync(ComicUpload comic);
        Task<Comic> GetComicByTitleAsync(string title);
        Task<Comic> ToComic(ComicUpload comicUpload);
    }
}
