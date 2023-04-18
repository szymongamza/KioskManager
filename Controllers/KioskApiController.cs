using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KioskManager.Data;
using KioskManager.Models;
using System.Security.Cryptography.Xml;
using System.Composition.Convention;

namespace KioskManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KioskApiController : ControllerBase
    {
        private readonly KioskManagerContext _context;

        public KioskApiController(KioskManagerContext context)
        {
            _context = context;
        }

        // GET: api/KioskApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kiosk>>> GetKiosk()
        {
          if (_context.Kiosk == null)
          {
              return NotFound();
          }
            return await _context.Kiosk.ToListAsync();
        }

        // GET: api/KioskApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kiosk>> GetKiosk(int id)
        {
          if (_context.Kiosk == null)
          {
              return NotFound();
          }
            var kiosk = await _context.Kiosk.FindAsync(id);

            if (kiosk == null)
            {
                return NotFound();
            }

            return kiosk;
        }

        [HttpGet("register")]
        public async Task<ActionResult<string>> Register([FromQuery] string kiosk)
        {
            var iPAddress = GetClientIpAddress(HttpContext);
            var hostIpAddress = HttpContext.Connection.LocalIpAddress.ToString();
            var kioskObj = await _context.Kiosk.FirstOrDefaultAsync(x => x.PCId == kiosk);
            if (kioskObj is null)
            {
                kioskObj = new Kiosk
                {
                    PCId = kiosk,
                    ActualIPAddress = iPAddress.ToString(),
                    SettingHostName = "DefaultHostName",
                    isOnline = false,
                    Registered = DateTime.Now,
                    SettingHomePage = $"http://{hostIpAddress}/Kiosk/Details/{kiosk}",
                    SettingKioskConfig = $"http://{hostIpAddress}/KioskApi/register",
                    SettingScheduledAction = "Monday-22:00 Tuesday-22:00 Wednesday-22:00 Thursday-22:00 Friday-22:00 Saturday-22:00 action:halt",
                    SettingRefreshPage = TimeSpan.FromSeconds(10),
                    SettingRootPassword = "password",
                    SettingRtcWake = "Monday-7:00 Tuesday-7:00 Wednesday-7:00 Thursday-7:00 Friday-7:00 Saturday-7:00",
                    SettingScreenSettings = "DP1:1920x1080:60.00:normal:right:normal DVI-0:1920x1080:60.00:normal:right:normal",
                    SettingTimeZone = "Europe/Warsaw"
                };
                await _context.Kiosk.AddAsync(kioskObj);
                await _context.SaveChangesAsync();
                return kioskObj.GetSettings();
            }
            kioskObj.ActualIPAddress = iPAddress.ToString();
            _context.Kiosk.Update(kioskObj);
            await _context.SaveChangesAsync();
            return kioskObj.GetSettings();
        }

        // PUT: api/KioskApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKiosk(int id, Kiosk kiosk)
        {
            if (id != kiosk.Id)
            {
                return BadRequest();
            }

            _context.Entry(kiosk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KioskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KioskApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kiosk>> PostKiosk(Kiosk kiosk)
        {
          if (_context.Kiosk == null)
          {
              return Problem("Entity set 'KioskManagerContext.Kiosk'  is null.");
          }
            _context.Kiosk.Add(kiosk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKiosk", new { id = kiosk.Id }, kiosk);
        }

        // DELETE: api/KioskApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKiosk(int id)
        {
            if (_context.Kiosk == null)
            {
                return NotFound();
            }
            var kiosk = await _context.Kiosk.FindAsync(id);
            if (kiosk == null)
            {
                return NotFound();
            }

            _context.Kiosk.Remove(kiosk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KioskExists(int id)
        {
            return (_context.Kiosk?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public string GetClientIpAddress(HttpContext context)
        {
            // Try to get the client IP address from the X-Real-IP header
            var clientIp = context.Request.Headers["X-Real-IP"];

            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = context.Request.Headers["X-Forwarded-For"];
            }

            // If the X-Real-IP header is not present, fall back to the RemoteIpAddress property
            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = context.Connection.RemoteIpAddress.ToString();
            }

            return clientIp;
        }
    }
}
