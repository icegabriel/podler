using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Podler.Models;
using Podler.Services;
using Podler.ViewModels;

namespace Podler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPodlerApiService _podlerApiService;

        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
                              IPodlerApiService podlerApiService,
                              IConfiguration configuration)
        {
            _logger = logger;
            _podlerApiService = podlerApiService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var api = _configuration["DependencesServicesUrl:PodlerApi"];

            var categories = await _podlerApiService.GetCategoriesAsync();
            var viewModel = new HomeViewModel(api, categories);

            return View(viewModel);
        }

        public async Task<IActionResult> Comic(int comicid)
        {
            var comicDb = await _podlerApiService.GetComicAsync(comicid);

            if (comicDb == null)
                return RedirectToAction("index");

            return View(comicDb);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
