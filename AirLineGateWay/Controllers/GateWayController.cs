using Microsoft.AspNetCore.Mvc;

namespace AirLineGateWay.Controllers
{
    public class GateWayController : Controller
    {
        [Route("GateWay/Login")]
        public string Login()
        {
            return "Login";
        }

        [Route("GateWay/Booking")]
        public string Booking()
        {
            return "Booking";
        }

        [Route("GateWay/Admin")]
        public string Admin()
        {
            return "Admin";
        }
    }
}
