using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly InventoryDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(InventoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> ListEntitiesAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception)
            {
                return new List<TEntity>();
            }
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(object id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    throw new Exception($"{nameof(entity)} does not exist in the database");

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual bool CreateEntity(TEntity entityToCreate)
        {
            try
            {
                _dbSet.Add(entityToCreate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool UpdateEntity(TEntity entityToUpdate)
        {
            try
            {
                _dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteEntityAsync(object id)
        {
            try
            {
                TEntity entity = await _dbSet.FindAsync(id) ??
                    throw new Exception("Entity does not exist in the database");

                return DeleteEntity(entity);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool DeleteEntity(TEntity entityToDelete)
        {
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
