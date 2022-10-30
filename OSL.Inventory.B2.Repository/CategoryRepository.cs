using Microsoft.Extensions.Logging;
using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Interfaces;

namespace OSL.Inventory.B2.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(InventoryDbContext context) :
            base(context)
        {
        }
    }
}
