using Microsoft.EntityFrameworkCore;
using MunicipalityTax.Models;

namespace MunicipalityTax.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// DbSet representing municipality table in my database.
        /// </summary>
        public DbSet<Municipality> Municipalities { get; set; }
        /// <summary>
        /// DbSet representing taxes table in my database. contains all the tax rates.
        /// </summary>
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
