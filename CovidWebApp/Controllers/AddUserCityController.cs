using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CovidWebApp.Models;
using CovidWebApp.Services;

namespace CovidWebApp.Controllers
{
    public class AddUserCityController : Controller
    {
        private readonly ICityService _cityService;
        public AddUserCityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(City c)
        {
            _cityService.AddUserInputCity(c);
            _cityService.SaveChanges();
            return RedirectToAction("Index", "Home" );
        }

    }
}
