using KioskManager.Data;
using Microsoft.EntityFrameworkCore;

namespace KioskManager.Services
{
    public class ConnectionCheckBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public ConnectionCheckBackgroundService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async void CheckConnectionOfAll()
        {
            using(var scope = scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<KioskManagerContext>();
                var kiosks = await _context.Kiosk.ToListAsync();
                foreach (var k in kiosks)
                {
                    var reply = await PingService.PingDevice(k.ActualIPAddress);
                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        k.isOnline = true;
                    }
                    else
                    {
                        k.isOnline = false;
                    }
                }
                _context.UpdateRange(kiosks);
                _context.SaveChanges();
            }
           

        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            await Task.Yield();

            while (token.IsCancellationRequested == false)
            {
                await Task.Delay(30000, token);
                CheckConnectionOfAll();
            }
        }
    }
}
