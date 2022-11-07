using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;

namespace OSL.Inventory.B2.Repository
{
    public interface IPurchaseRepository : IBaseRepository<Purchase>    
    {

    }

    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        private readonly InventoryDbContext _context;
        public PurchaseRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        #region SingleInstance
        #endregion

        #region ListInstance
        #endregion

        #region Operations
        #endregion
    }
}
