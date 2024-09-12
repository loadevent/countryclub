using CountryClub.Data;
using Microsoft.AspNetCore.Mvc;
using CountryClub.Models;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Controllers
{
    public class LoginController : Controller
    {
        private readonly CountryClubDbContext _context;

        public LoginController(CountryClubDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public async  Task<IActionResult> ValidateLogin()
        {
            //String userId = Request.Form["txtUserId"].ToString();
            String clientId = Request.Form["txtUserId"].ToString();
            String password = Request.Form["txtPassword"].ToString();
           // var ad = await _context.Admins.FirstOrDefaultAsync(a => a.UserId.Equals(userId));
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId.Equals(clientId));

			var countryClubDbContext = _context.ProvidedServices.Include(p => p.Provider).Include(p => p.Service);

			if (client == null)
            {
                return NotFound();
            }
            else
            {
                if (clientId.Equals(client.ClientId) && password.Equals(client.Password))
                {
                   // return RedirectToAction("Index", "Services", await _context.Services.ToListAsync());
                    return RedirectToAction("Index", "ProvidedServices", await _context.ProvidedServices.Include(p => p.Provider).Include(p => p.Service).ToListAsync());
                }
                else
                {
                    return View("Login");
                }
            }
            
            //return View("Index");
        }
           

    }
}
