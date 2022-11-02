using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly InventoryDbContext _context;

        public CategoryRepository(InventoryDbContext context) :
            base(context)
        {
            _context = context;
        }

        public async Task<bool> SoftDeleteEntity(long id)
        {
            try
            {
                var entity = await _context.Categories.FindAsync(id) ??
                    throw new Exception("Category does not exist in the database");

                entity.Status = Entity.Enums.Status.Deleted;
                entity.ModifiedAt = DateTime.Now;
                entity.ModifiedBy = 2;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
