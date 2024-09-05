using Microsoft.AspNetCore.Mvc;

namespace CountryClub.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }
    }
}
