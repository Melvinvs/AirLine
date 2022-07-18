using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers
{
    [Route("Flight")]
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
