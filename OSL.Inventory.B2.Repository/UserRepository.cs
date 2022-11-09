using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;

namespace OSL.Inventory.B2.Repository
{
    public interface IUserRepository : IBaseRepository<InventoryUser>
    {

    }

    public class UserRepository : BaseRepository<InventoryUser>, IUserRepository
    {
        public UserRepository(InventoryDbContext context) : base(context)
        {
        }
    }
}
