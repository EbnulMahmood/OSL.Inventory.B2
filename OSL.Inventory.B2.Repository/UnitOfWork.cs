using Microsoft.Extensions.Logging;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Data.Interfaces;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool disposed = false;
        private readonly InventoryDbContext _context;

        public ICategoryRepository CategoryRepository { get; private set; }

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;

            CategoryRepository = new CategoryRepository(context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
