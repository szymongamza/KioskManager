using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KioskManager.Data;
using KioskManager.Models;
using System.Diagnostics;

namespace KioskManager.Controllers
{
    public class KioskController : Controller
    {
        private readonly KioskManagerContext _context;

        public KioskController(KioskManagerContext context)
        {
            _context = context;
        }

        // GET: Kiosk
        public async Task<IActionResult> Index()
        {
              return _context.Kiosk != null ? 
                          View(await _context.Kiosk.ToListAsync()) :
                          Problem("Entity set 'KioskManagerContext.Kiosk'  is null.");
        }

        // GET: Kiosk/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Kiosk == null)
            {
                return NotFound();
            }

            var kioskObj = await _context.Kiosk
                .FirstOrDefaultAsync(m => m.PCId == id);

            if (kioskObj == null)
            {
                return NotFound();
            }

            return View(kioskObj);
        }

        // GET: Kiosk/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kiosk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PCId,ActualIPAddress,isOnline,Registered,LastOnline,SettingHostName,SettingHomePage,SettingKioskConfig,SettingScheduledAction,SettingRefreshPage,SettingRootPassword,SettingRtcWake,SettingScreenSettings,SettingTimeZone")] Kiosk kiosk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kiosk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kiosk);
        }

        // GET: Kiosk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kiosk == null)
            {
                return NotFound();
            }

            var kiosk = await _context.Kiosk.FindAsync(id);
            if (kiosk == null)
            {
                return NotFound();
            }
            return View(kiosk);
        }

        // POST: Kiosk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PCId,ActualIPAddress,isOnline,Registered,LastOnline,SettingHostName,SettingHomePage,SettingKioskConfig,SettingScheduledAction,SettingRefreshPage,SettingRootPassword,SettingRtcWake,SettingScreenSettings,SettingTimeZone")] Kiosk kiosk)
        {
            if (id != kiosk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kiosk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KioskExists(kiosk.Id))
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
            return View(kiosk);
        }

        // GET: Kiosk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kiosk == null)
            {
                return NotFound();
            }

            var kiosk = await _context.Kiosk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kiosk == null)
            {
                return NotFound();
            }

            return View(kiosk);
        }

        // POST: Kiosk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kiosk == null)
            {
                return Problem("Entity set 'KioskManagerContext.Kiosk'  is null.");
            }
            var kiosk = await _context.Kiosk.FindAsync(id);
            if (kiosk != null)
            {
                _context.Kiosk.Remove(kiosk);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool KioskExists(int id)
        {
          return (_context.Kiosk?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
