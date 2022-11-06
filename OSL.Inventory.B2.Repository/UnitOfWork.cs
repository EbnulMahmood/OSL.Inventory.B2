using OSL.Inventory.B2.Repository.Data;
using System;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        void Dispose();
        Task<bool> SaveAsync();
    }

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool _disposed = false;
        private readonly InventoryDbContext _context;

        public ICategoryRepository CategoryRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;

            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context);
        }

        public async Task<bool> SaveAsync()
        {
            bool returnValue = true;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }
            return returnValue;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
