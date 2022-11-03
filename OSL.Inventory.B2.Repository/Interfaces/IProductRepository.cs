using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<(IEnumerable<Product>, int, int)> ListProductsWithSortingFilteringPagingAsync(int start, int length, 
            string order, string orderDir, string searchByName, Status filterByStatus = 0);
        Task<bool> SoftDeleteEntity(long id);
    }
}