using System.ComponentModel.DataAnnotations.Schema;
using LogWire.SIEM.Service.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LogWire.SIEM.Service.Data.Context
{
    public class DataContext : DbContext
    {

        public DbSet<UserEntry> Users { get; set; }
        public DbSet<MachineEntry> Machines { get; set; }
        public DbSet<AuthEventEntry> AuthEvents { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
