using KioskManager.Data;
using Microsoft.EntityFrameworkCore;

namespace KioskManager.Services
{
    public class ConnectionCheckBackgroundService
    {
        private readonly KioskManagerContext _context;

        public ConnectionCheckBackgroundService(KioskManagerContext context)
        {
            _context = context;
        }

        public async void CheckConnectionOfAll()
        {
            var kiosks = await _context.Kiosk.ToListAsync();
            foreach(var k in kiosks)
            {
                var reply = PingService.PingDevice(k.ActualIPAddress);
            }

        }
    }
}
