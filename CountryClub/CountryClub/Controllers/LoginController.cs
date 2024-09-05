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
            String userId = Request.Form["txtUserId"].ToString();
            String password = Request.Form["txtPassword"].ToString();
            var ad = await _context.Admins.FirstOrDefaultAsync(a => a.UserId.Equals(userId));

            if (ad == null)
            {
                return NotFound();
            }
            else
            {
                if (userId.Equals(ad.UserId) && password.Equals(ad.Password))
                {
                    return RedirectToAction("Index", "Services", await _context.Services.ToListAsync());
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
