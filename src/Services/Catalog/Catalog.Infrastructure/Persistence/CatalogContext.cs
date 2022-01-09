using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.API.Persistence
{
    public class CatalogContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=catalogdb", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure with fluent api 
            // https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            // Sales
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(o => o.Id).HasDefaultValue(0).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>().Property(o => o.Name).IsRequired().HasMaxLength(175);
            // https://en.wikipedia.org/wiki/ISO_4217
            modelBuilder.Entity<Product>().Property(o => o.Currency).IsRequired().HasMaxLength(3);
            modelBuilder.Entity<Product>().Property(o => o.Price).IsRequired().HasPrecision(8, 2);
        }
    }
}
