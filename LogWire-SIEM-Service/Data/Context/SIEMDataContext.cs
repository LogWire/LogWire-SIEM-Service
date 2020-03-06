using LogWire.SIEM.Service.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LogWire.SIEM.Service.Data.Context
{
    public class SIEMDataContext : DbContext
    {

        public DbSet<SIEMUserEntry> Users { get; set; }

        public SIEMDataContext(DbContextOptions<SIEMDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
