using Microsoft.Extensions.Logging;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool _disposed = false;
        private readonly InventoryDbContext _context;

        public ICategoryRepository CategoryRepository { get; private set; }

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;

            CategoryRepository = new CategoryRepository(context);
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
