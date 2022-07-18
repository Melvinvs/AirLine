using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("Booking")]
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
