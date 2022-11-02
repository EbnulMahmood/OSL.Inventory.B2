using OSL.Inventory.B2.Entity;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> SoftDeleteEntity(long id);
    }
}
