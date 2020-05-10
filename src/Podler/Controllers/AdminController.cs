using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
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

        public AdminController(ILogger<HomeController> logger, IPodlerApiService podlerApiService)
        {
            _logger = logger;
            _podlerApiService = podlerApiService;
        }

        public async Task<IActionResult> AddComic()
        {
            var categories = await _podlerApiService.GetCategoriesAsync();
            var designers = await _podlerApiService.GetDesignersAsync();
            var publishers = new List<Publisher>();
            var authors = await _podlerApiService.GetAuthorsAsync();

            var viewModel = new AddComicViewModel(categories, authors, designers, publishers);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComic(AddComicViewModel addComicViewModel)
        {
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<SelectAddResponse> AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return new SelectAddResponse(false, "Categoria invalida.");
            }

            return await _podlerApiService.AddCategory(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<SelectAddResponse> AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return new SelectAddResponse(false, "Autor invalido.");
            }

            return await _podlerApiService.AddAuthor(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<SelectAddResponse> AddDesigner([FromBody] Designer designer)
        {
            if (!ModelState.IsValid)
            {
                return new SelectAddResponse(false, "Desenhista invalido.");
            }

            return await _podlerApiService.AddDesigner(designer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}