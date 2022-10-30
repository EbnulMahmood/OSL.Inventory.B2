using OSL.Inventory.B2.Entity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Data
{
    public sealed class InventoryDbContext : DbContext
    {
        public InventoryDbContext() : base("InventoryConnection")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            // category constraints
            builder.Entity<Category>().Property(c => c.Name).HasMaxLength(255);
            builder.Entity<Category>().Property(c => c.Description).HasMaxLength(500);

            // User constraints
            builder.Entity<User>().Property(c => c.FirstName).HasMaxLength(255);
            builder.Entity<User>().Property(c => c.LastName).HasMaxLength(255);
            builder.Entity<User>().Property(c => c.Country).HasMaxLength(50);
            builder.Entity<User>().Property(c => c.City).HasMaxLength(80);
            builder.Entity<User>().Property(c => c.State).HasMaxLength(80);
            builder.Entity<User>().Property(c => c.ZipCode).HasMaxLength(20);
        }
    }
}
