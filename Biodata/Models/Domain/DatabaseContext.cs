using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Biodata.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts)
        {

        }
        public DbSet<Profil> Profil { get; set; }
    }
}
