using Microsoft.EntityFrameworkCore;
using TaxRateScheduler.Model;

namespace TaxRateScheduler.Context
{
    public class MunicipalityTaxRateContext : DbContext
    {
        public MunicipalityTaxRateContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaxRateModel> tblTaxRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxRateModel>()
                .HasKey(c => new { c.MunicipalityName, c.ScheduleType, c.StartDate });
        }
    }
}
