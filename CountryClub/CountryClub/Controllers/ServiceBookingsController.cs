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
    public class ServiceBookingsController : Controller
    {
        private readonly CountryClubDbContext _context;

        public ServiceBookingsController(CountryClubDbContext context)
        {
            _context = context;
        }

        // GET: ServiceBookings
        public async Task<IActionResult> Index()
        {
            var countryClubDbContext = _context.ServiceBookings.Include(s => s.Admin).Include(s => s.Client).Include(s => s.Service);
            return View(await countryClubDbContext.ToListAsync());
        }

        // GET: ServiceBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceBooking = await _context.ServiceBookings
                .Include(s => s.Admin)
                .Include(s => s.Client)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (serviceBooking == null)
            {
                return NotFound();
            }

            return View(serviceBooking);
        }

        // GET: ServiceBookings/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Admins, "UserId", "UserId");
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId");
            return View();
        }

        // POST: ServiceBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,ServiceId,ClientId,BookingDate,AdminId")] ServiceBooking serviceBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "UserId", "UserId", serviceBooking.AdminId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", serviceBooking.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", serviceBooking.ServiceId);
            return View(serviceBooking);
        }

        // GET: ServiceBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceBooking = await _context.ServiceBookings.FindAsync(id);
            if (serviceBooking == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "UserId", "UserId", serviceBooking.AdminId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", serviceBooking.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", serviceBooking.ServiceId);
            return View(serviceBooking);
        }

        // POST: ServiceBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,ServiceId,ClientId,BookingDate,AdminId")] ServiceBooking serviceBooking)
        {
            if (id != serviceBooking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceBookingExists(serviceBooking.BookingId))
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
            ViewData["AdminId"] = new SelectList(_context.Admins, "UserId", "UserId", serviceBooking.AdminId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", serviceBooking.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", serviceBooking.ServiceId);
            return View(serviceBooking);
        }

        // GET: ServiceBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceBooking = await _context.ServiceBookings
                .Include(s => s.Admin)
                .Include(s => s.Client)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (serviceBooking == null)
            {
                return NotFound();
            }

            return View(serviceBooking);
        }

        // POST: ServiceBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceBooking = await _context.ServiceBookings.FindAsync(id);
            if (serviceBooking != null)
            {
                _context.ServiceBookings.Remove(serviceBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceBookingExists(int id)
        {
            return _context.ServiceBookings.Any(e => e.BookingId == id);
        }
    }
}
