using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using System.Reflection;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=orderingdb", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        // TODO : implement
                        entry.Entity.CreatedBy = 0;
                        entry.Entity.CreatedAt = DateTime.Now;

                        break;
                    case EntityState.Modified:
                        // TODO : implement
                        entry.Entity.LastModifiedBy = 0;
                        entry.Entity.LastModifiedAt = DateTime.Now;

                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure with fluent api 
            // https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            // Sales
            modelBuilder.Entity<Sales>().HasKey(x => x.Id);
            modelBuilder.Entity<Sales>().Property(o => o.Id).HasDefaultValue(0).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<Sales>().Property(o => o.CustomerId).HasDefaultValue(0).IsRequired();
            modelBuilder.Entity<Sales>().Property(o => o.DocDate).IsRequired();
            modelBuilder.Entity<Sales>().Property(o => o.DocNo).IsRequired().HasMaxLength(12);
            modelBuilder.Entity<Sales>().Property(o => o.Total).HasPrecision(8, 2).IsRequired();
            modelBuilder.Entity<Sales>().Property(o => o.Total).HasPrecision(8, 2).IsRequired();
            // https://en.wikipedia.org/wiki/ISO_4217
            modelBuilder.Entity<Sales>().Property(o => o.Currency).IsRequired().HasMaxLength(3);
            modelBuilder.Entity<Sales>().Property(o => o.CreatedBy).IsRequired();
            modelBuilder.Entity<Sales>().Property(o => o.CreatedAt).IsRequired();
            modelBuilder.Entity<Sales>().Property(o => o.LastModifiedBy);
            modelBuilder.Entity<Sales>().Property(o => o.LastModifiedAt);

            // Sales Detail
            modelBuilder.Entity<SalesItem>().HasKey(x => x.Id);
            modelBuilder.Entity<SalesItem>().Property(o => o.Id).HasDefaultValue(0).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<SalesItem>().Property(o => o.OrderId).IsRequired();
            modelBuilder.Entity<SalesItem>().Property(o => o.ProductId).IsRequired();
            modelBuilder.Entity<SalesItem>().Property(o => o.Quantity).IsRequired();
            modelBuilder.Entity<SalesItem>().Property(o => o.Price).IsRequired().HasPrecision(8, 2);
            modelBuilder.Entity<SalesItem>().Property(o => o.CreatedBy).IsRequired();
            modelBuilder.Entity<SalesItem>().Property(o => o.CreatedAt).IsRequired();
            modelBuilder.Entity<SalesItem>().Property(o => o.LastModifiedBy);
            modelBuilder.Entity<SalesItem>().Property(o => o.LastModifiedAt);

            // FK
            modelBuilder.Entity<Sales>().HasMany(o => o.Items).WithOne(o => o.Sales).HasForeignKey(o => o.OrderId);
        }
    }
}
