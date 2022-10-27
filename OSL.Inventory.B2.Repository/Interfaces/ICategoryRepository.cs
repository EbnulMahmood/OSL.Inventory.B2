using OSL.Inventory.B2.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(long? entityToGetId);
        Task<Category> CreateCategoryAsync(Category entityToCreate);
        Task<Category> UpdateCategoryAsync(Category entityToUpdate);
        Task<bool> DeleteCategoryByIdAsync(long entityToDeleteId);
    }
}