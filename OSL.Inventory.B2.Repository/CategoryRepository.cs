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
        
        public async Task<Category> GetCategoryByIdAsync(long id)
        {
            try
            {
                return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CreateCategoryAsync(Category entityToCreate)
        {
            try
            {
                var entity = _context.Categories.Add(entityToCreate);
                if (entity == null) throw new Exception();
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }

        public Task<bool> UpdateCategoryAsync(Category entity)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> DeleteCategoryByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
