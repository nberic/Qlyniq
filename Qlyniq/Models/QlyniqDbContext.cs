using Microsoft.EntityFrameworkCore;

using Qlyniq.Models.Data;

namespace Qlyniq.Models
{
    public class QlyniqDbContext : DbContext
    {
        public QlyniqDbContext(DbContextOptions<QlyniqDbContext> options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
    }
}