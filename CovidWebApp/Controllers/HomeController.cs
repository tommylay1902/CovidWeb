using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CovidWebApp.Models;
using CovidWebApp.Services;

namespace CovidWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICityService _cityService;

        public HomeController(ILogger<HomeController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        public IActionResult Index(int? id = null)
        {
            if (_cityService.IsEmpty()) return View();
            else
            {
                if (id == null)
                {
                    _logger.LogInformation("Entered the if");
                    return View(_cityService.GetCity(1));
                }
                else
                {
                    return View(_cityService.GetCity((int)id));
                }
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
