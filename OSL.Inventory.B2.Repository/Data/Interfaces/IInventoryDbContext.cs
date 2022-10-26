using OSL.Inventory.B2.Entity;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Data.Interfaces
{
    public interface IInventoryDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}