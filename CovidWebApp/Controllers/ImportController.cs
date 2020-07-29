using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidWebApp.Controllers
{
    public class ImportController : Controller
    {
        private readonly ICityService _cityService;
        public ImportController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet]
        public IActionResult Menu()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Menu(IFormFile file, DateTime date)
        {
            _cityService.ImportCityData(file, date);
            return RedirectToAction("Index", "Home");
        }
    }
}
