using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> SoftDeleteEntity(long id);
        Task<(IEnumerable<Category>, int, int)> ListCategoriesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0);
    }
}
