using Microsoft.EntityFrameworkCore;
using StoredProcedure.Models;

namespace StoredProcedure.Data
{
    public class StoredProcedureDbContext : DbContext
    {
        public StoredProcedureDbContext(DbContextOptions<StoredProcedureDbContext> options)
            : base(options) { }

        public DbSet<EmployeeViewModel> Employee { get; set; }
    }
}
