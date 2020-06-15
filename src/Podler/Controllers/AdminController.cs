using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Podler.Controllers;
using Podler.Models;
using Podler.Responses;
using Podler.Services;
using Podler.ViewModels;

namespace Podler.Views
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPodlerApiService _podlerApiService;

        public AdminController(ILogger<HomeController> logger,
                               IPodlerApiService podlerApiService)
        {
            _logger = logger;
            _podlerApiService = podlerApiService;
        }

        public async Task<IActionResult> AddComic()
        {
            var categories = await _podlerApiService.GetCategoriesAsync();
            var designers = await _podlerApiService.GetDesignersAsync();
            var publishers = await _podlerApiService.GetPublishersAsync();
            var authors = await _podlerApiService.GetAuthorsAsync();

            var viewModel = new AddComicViewModel(categories, authors, designers, publishers);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComic(AddComicViewModel addComicViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            addComicViewModel.SetComicUploadCover();

            if (addComicViewModel.Comic.Cover.Length > 5242880)
                return BadRequest();

            var response = await _podlerApiService.PostComic(addComicViewModel.Comic);

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<SelectAddResponse> AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return new SelectAddResponse(false, "Categoria invalida.");
            }

            return await _podlerApiService.AddCategoryAsync(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<SelectAddResponse> AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return new SelectAddResponse(false, "Autor invalido.");
            }

            return await _podlerApiService.AddAuthorAsync(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<SelectAddResponse> AddDesigner([FromBody] Designer designer)
        {
            if (!ModelState.IsValid)
            {
                return new SelectAddResponse(false, "Desenhista invalido.");
            }

            return await _podlerApiService.AddDesignerAsync(designer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<SelectAddResponse> AddPublisher([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return new SelectAddResponse(false, "Editora invalida.");
            }

            return await _podlerApiService.AddPublisherAsync(publisher);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChapter(int comicid)
        {
            var comicDb = await _podlerApiService.GetComicAsync(comicid);

            if (comicDb != null)
            {
                return View(comicDb);
            }

            return RedirectToAction("index", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}