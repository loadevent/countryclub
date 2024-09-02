using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CountryClub.Data;
using CountryClub.Models;

namespace CountryClub.Controllers
{
    public class AdminsController : Controller
    {
        private readonly CountryClubDbContext _context;

        public AdminsController(CountryClubDbContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CellphoneNo,Firstname,Lastname,Email,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,CellphoneNo,Firstname,Lastname,Email,Password")] Admin admin)
        {
            if (id != admin.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(string id)
        {
            return _context.Admins.Any(e => e.UserId == id);
        }
    }
}
