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
        public DbSet<InventoryUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

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
            builder.Entity<InventoryUser>().Property(c => c.FirstName).HasMaxLength(255);
            builder.Entity<InventoryUser>().Property(c => c.LastName).HasMaxLength(255);
            builder.Entity<InventoryUser>().Property(c => c.Country).HasMaxLength(50);
            builder.Entity<InventoryUser>().Property(c => c.City).HasMaxLength(80);
            builder.Entity<InventoryUser>().Property(c => c.State).HasMaxLength(80);
            builder.Entity<InventoryUser>().Property(c => c.ZipCode).HasMaxLength(20);

            // product constraints
            builder.Entity<Product>().Property(x => x.Name).HasMaxLength(255);
            builder.Entity<Product>().Property(x => x.Description).HasMaxLength(500);
            builder.Entity<Product>().Property(x => x.ImageUrl).HasMaxLength(255);

            // suppliers constraints
            builder.Entity<Supplier>().Property(c => c.FirstName).HasMaxLength(255);
            builder.Entity<Supplier>().Property(c => c.LastName).HasMaxLength(255);
            builder.Entity<Supplier>().Property(c => c.EmailAddress).HasMaxLength(50);
            builder.Entity<Supplier>().Property(c => c.PhoneNumber).HasMaxLength(30);
            builder.Entity<Supplier>().Property(c => c.Country).HasMaxLength(50);
            builder.Entity<Supplier>().Property(c => c.City).HasMaxLength(80);
            builder.Entity<Supplier>().Property(c => c.State).HasMaxLength(80);
            builder.Entity<Supplier>().Property(c => c.ZipCode).HasMaxLength(20);

            // Purchases constraints
            builder.Entity<Purchase>().Property(c => c.PurchaseCode).HasMaxLength(255);

            // Customers constraints
            builder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(255);
            builder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(255);
            builder.Entity<Customer>().Property(c => c.EmailAddress).HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.PhoneNumber).HasMaxLength(30);
            builder.Entity<Customer>().Property(c => c.Country).HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.City).HasMaxLength(80);
            builder.Entity<Customer>().Property(c => c.State).HasMaxLength(80);
            builder.Entity<Customer>().Property(c => c.ZipCode).HasMaxLength(20);

            // Sales constraints
            builder.Entity<Sale>().Property(c => c.SaleCode).HasMaxLength(255);
        }
    }
}
