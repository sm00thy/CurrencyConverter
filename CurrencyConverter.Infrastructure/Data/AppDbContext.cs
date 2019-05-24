using CurrencyConverter.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
        public DbSet<CurrencyTable> CurrencyTables { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<CurrencyConvert> CurrencyConverts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrencyTable>()
                .HasMany(a => a.Rates)
                .WithOne(b => b.CurrencyTable);
            modelBuilder.Entity<Rate>()
                .HasOne(a => a.CurrencyTable)
                .WithMany();
        }
    }
}