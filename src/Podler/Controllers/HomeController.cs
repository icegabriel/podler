using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Podler.Models;
using Podler.Services;

namespace Podler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPodlerApiService _podlerApiService;

        public HomeController(ILogger<HomeController> logger, IPodlerApiService podlerApiService)
        {
            _logger = logger;
            _podlerApiService = podlerApiService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _podlerApiService.GetCategoriesAsync();

            return View(categories);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
