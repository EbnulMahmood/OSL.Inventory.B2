using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data.Interfaces;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Data
{
    public sealed class InventoryDbContext : DbContext, IInventoryDbContext
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
    }
}
