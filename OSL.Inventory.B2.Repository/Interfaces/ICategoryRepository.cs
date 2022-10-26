using OSL.Inventory.B2.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(long id);
        Task<bool> CreateCategoryAsync(Category entity);
        Task<bool> UpdateCategoryAsync(Category entity);
        Task<bool> DeleteCategoryByIdAsync(long id);
    }
}