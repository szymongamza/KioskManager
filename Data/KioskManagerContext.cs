using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KioskManager.Models;

namespace KioskManager.Data
{
    public class KioskManagerContext : DbContext
    {
        public KioskManagerContext (DbContextOptions<KioskManagerContext> options)
            : base(options)
        {
        }

        public DbSet<KioskManager.Models.Kiosk> Kiosk { get; set; } = default!;
    }
}
