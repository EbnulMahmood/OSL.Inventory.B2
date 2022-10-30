using OSL.Inventory.B2.Repository.Interfaces;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }

        void Dispose();
        Task SaveAsync();
    }
}