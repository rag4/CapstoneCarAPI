using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarDelershipMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarDelershipMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CarDAL cd = new CarDAL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data()
        {
            List<Cars> cars = cd.GetCars();
            return View(cars);
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Results(string Keyword, string Query)
        {
            List<Cars> cars = cd.GetCarsExt(Keyword, Query);
            return View(cars);
        }

        public IActionResult AdvanceSearch()
        {
            return View();
        }

        public IActionResult AdvanceSearchResults(string Make, string Model, string Year, string Color)
        {
            List<Cars> cars = cd.GetCars();
            if (!string.IsNullOrWhiteSpace(Make))
            {
                cars = cars.Where(x => x.make == Make).ToList();
            }
            if (!string.IsNullOrWhiteSpace(Model))
            {
                cars = cars.Where(x => x.model == Model).ToList();
            }
            if (!string.IsNullOrWhiteSpace(Year))
            {
                cars = cars.Where(x => x.year == int.Parse(Year)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(Color))
            {
                cars = cars.Where(x => x.color == Color).ToList();
            }
            return View(cars);
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
