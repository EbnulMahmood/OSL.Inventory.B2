using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Data.Interfaces;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IInventoryDbContext _context;

        public CategoryRepository(IInventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> ListCategoriesAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
