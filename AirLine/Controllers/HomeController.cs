using AirLine.Interface;
using AirLine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AirLine.Controllers
{
    public class HomeController : Controller
    {
        private IFlightService _flight;

        public HomeController(IFlightService flight)
        {
            this._flight = flight;
        }

        public IActionResult Index()
        {
            return View();
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

        [Route("Flight/Name")]
        public string GetName()
        {
            return _flight.GetFlightDetails(1).AirLineName;
        }
    }
}
