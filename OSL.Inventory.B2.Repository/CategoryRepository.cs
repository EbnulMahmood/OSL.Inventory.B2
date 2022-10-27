using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data.Interfaces;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public sealed class CategoryRepository : ICategoryRepository
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
        
        public async Task<Category> GetCategoryByIdAsync(long? entityToGetId)
        {
            try
            {
                return await _context.Categories.FirstOrDefaultAsync(x => x.Id == entityToGetId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> CreateCategoryAsync(Category entityToCreate)
        {
            try
            {
                var entity = _context.Categories.Add(entityToCreate);
                if (entity == null) throw new Exception();
                
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> UpdateCategoryAsync(Category entityToUpdate)
        {
            try
            {
                var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == entityToUpdate.Id);
                if (entity == null) throw new Exception();
                
                entity.Name = entityToUpdate.Name;
                entity.Description = entityToUpdate.Description;
                entity.Status = entityToUpdate.Status;

                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public async Task<bool> DeleteCategoryByIdAsync(long entityToDeleteId)
        {
            try
            {
                var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == entityToDeleteId);
                if (entity == null) throw new Exception();

                _context.Categories.Remove(entity);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
