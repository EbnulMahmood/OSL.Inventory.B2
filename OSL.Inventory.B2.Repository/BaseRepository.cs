using OSL.Inventory.B2.Repository.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private readonly IInventoryDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(IInventoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
    }
}
