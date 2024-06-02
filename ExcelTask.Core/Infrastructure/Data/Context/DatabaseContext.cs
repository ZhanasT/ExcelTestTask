using ExcelTask.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExcelTask.Core.Infrastructure.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductGroups)
                .WithMany(pg => pg.Products)
                .UsingEntity<ProductGroupRelation>();

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<UnitOfMeasureType> UnitOfMeasureTypes { get; set; }
        public virtual DbSet<ProductGroupRelation> ProductGroupRelations { get; set; }
    }
}
