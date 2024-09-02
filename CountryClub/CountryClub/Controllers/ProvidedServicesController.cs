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
    public class ProvidedServicesController : Controller
    {
        private readonly CountryClubDbContext _context;

        public ProvidedServicesController(CountryClubDbContext context)
        {
            _context = context;
        }

        // GET: ProvidedServices
        public async Task<IActionResult> Index()
        {
            var countryClubDbContext = _context.ProvidedServices.Include(p => p.Provider).Include(p => p.Service);
            return View(await countryClubDbContext.ToListAsync());
        }

        // GET: ProvidedServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providedService = await _context.ProvidedServices
                .Include(p => p.Provider)
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (providedService == null)
            {
                return NotFound();
            }

            return View(providedService);
        }

        // GET: ProvidedServices/Create
        public IActionResult Create()
        {
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId");
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId");
            return View();
        }

        // POST: ProvidedServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ProviderId,ServiceRate")] ProvidedService providedService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(providedService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId", providedService.ProviderId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", providedService.ServiceId);
            return View(providedService);
        }

        // GET: ProvidedServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providedService = await _context.ProvidedServices.FindAsync(id);
            if (providedService == null)
            {
                return NotFound();
            }
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId", providedService.ProviderId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", providedService.ServiceId);
            return View(providedService);
        }

        // POST: ProvidedServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ProviderId,ServiceRate")] ProvidedService providedService)
        {
            if (id != providedService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(providedService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvidedServiceExists(providedService.ServiceId))
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
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId", providedService.ProviderId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", providedService.ServiceId);
            return View(providedService);
        }

        // GET: ProvidedServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providedService = await _context.ProvidedServices
                .Include(p => p.Provider)
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (providedService == null)
            {
                return NotFound();
            }

            return View(providedService);
        }

        // POST: ProvidedServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var providedService = await _context.ProvidedServices.FindAsync(id);
            if (providedService != null)
            {
                _context.ProvidedServices.Remove(providedService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvidedServiceExists(int id)
        {
            return _context.ProvidedServices.Any(e => e.ServiceId == id);
        }
    }
}
