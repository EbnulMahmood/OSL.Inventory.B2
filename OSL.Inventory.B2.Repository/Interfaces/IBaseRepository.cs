using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        bool CreateEntity(TEntity entityToCreate);
        bool DeleteEntity(TEntity entityToDelete);
        Task<bool> DeleteEntityAsync(object id);
        Task<TEntity> GetEntityByIdAsync(object id);
        Task<IEnumerable<TEntity>> ListEntitiesAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        bool UpdateEntity(TEntity entityToUpdate);
    }
}