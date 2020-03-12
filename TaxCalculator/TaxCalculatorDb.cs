using Microsoft.EntityFrameworkCore;


namespace TaxCalculator.API.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<TaxTypePerPostalCode> TaxTypePerPostalCode { get; set; }
        public DbSet<ProgressiveTaxTable> ProgressiveTaxTables { get; set; }
        public DbSet<UserPayableTax> UserPayableTaxes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
