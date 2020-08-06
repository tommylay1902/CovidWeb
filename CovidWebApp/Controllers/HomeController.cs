using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CovidWebApp.Models;
using CovidWebApp.Services;
using Microsoft.Extensions.Configuration;

namespace CovidWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICityService _cityService;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, ICityService cityService, IConfiguration config)
        {
            _logger = logger;
            _cityService = cityService;
            _config = config;
        }
        public IActionResult Index()
        {
            ViewBag.Cities = _cityService.GetCities();
            string idNum = Request.Query["CitySelect"];
           
            if(idNum== null)
            {
                ViewBag.CityId = 1;
                ViewBag.Dates = _cityService.GetCaseDataDates(1);
                ViewBag.ViewSelectOption = 1;
                ViewBag.ViewOption = _cityService.GetCaseDataCases(1);
                return View(_cityService.GetCityCaseData(1));
            }
            else
            {
                
                int id = int.Parse(idNum);
                ViewBag.CityId = id;

                int viewSelectOption = int.Parse(Request.Query["ViewSelect"]);
                ViewBag.Dates = _cityService.GetCaseDataDates(id);
                if (viewSelectOption ==  1)
                {
                    ViewBag.ViewSelectOption = 1;
                    ViewBag.ViewOption = _cityService.GetCaseDataCases(id);
                }
                else if (viewSelectOption == 2)
                {
                    ViewBag.ViewSelectOption = 2;
                    ViewBag.ViewOption = _cityService.GetCaseDataDeaths(id);
                }
                else
                {
                    ViewBag.ViewSelectOption = 3;
                    ViewBag.ViewOption = _cityService.GetCaseDataTested(id);
                }
                return View(_cityService.GetCityCaseData(id));

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
