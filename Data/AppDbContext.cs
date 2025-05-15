using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Models;

namespace MunicipalityTax.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>()
                .HasMany(m => m.Taxes)
                .WithOne(t => t.Municipality)
                .HasForeignKey(t => t.MunicipalityId);
        }
    }
}
