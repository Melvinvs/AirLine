using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
