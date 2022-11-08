using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> SoftDeleteEntity(long id);
        Task<(IEnumerable<Category>, int, int)> ListCategoriesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0);
        IQueryable<Category> ListCategoriesDropdown();
        Task<IEnumerable<Category>> ListCategoriesByNameAsync(string name);
    }

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly InventoryDbContext _context;

        public CategoryRepository(InventoryDbContext context) :
            base(context)
        {
            _context = context;
        }

        #region SingleInstance
        #endregion

        #region ListInstance
        public async Task<IEnumerable<Category>> ListCategoriesByNameAsync(string name)
        {
            var entities = await _context.Categories
                    .ToListAsync();
            return entities;

        }

        public IQueryable<Category> ListCategoriesDropdown() { return _context.Categories; }

        #region Sorting
        // sort by order desc
        private IEnumerable<Category> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Category> data)
        {
            // Initialization.   
            IEnumerable<Category> sortedEntities = Enumerable.Empty<Category>();

            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Name)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) :
                            data.OrderBy(p => p.Name)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            });
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Status)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) :
                            data.OrderBy(p => p.Status)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            });
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.CreatedAt)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) :
                            data.OrderBy(p => p.CreatedAt)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            });
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.   
                Console.Write(ex);
            }
            // info.   
            return sortedEntities;
        }
        #endregion

        private Category SelectCategory(Category category)
        {
            return new Category()
            {
                Id = category.Id,
                Name = category.Name,
                Status = category.Status,
            };
        }

        public async Task<(IEnumerable<Category>, int, int)> ListCategoriesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Products.CountAsync();
            // get filtered count
            var recordCount = await _context.Products.CountAsync(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.Name.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0));

            IEnumerable<Category> listEntites = (await _context.Categories
                                                .Where(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.Name.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0))
                                                .OrderByDescending(d => d.CreatedAt)
                                                .Skip(start).Take(length)
                                                .ToListAsync())
                                                .Select(category => SelectCategory(category));

            // Sorting 
            var result = SortByColumnWithOrder(order, orderDir, listEntites);

            return (result, totalRecord, recordCount);
        }

        #endregion

        #region Operations
        public async Task<bool> SoftDeleteEntity(long id)
        
        {
            try
            {
                var entity = await _context.Categories.FindAsync(id) ??
                    throw new Exception("Category does not exist in the database");

                entity.Status = Status.Deleted;
                entity.ModifiedAt = DateTime.Now;
                entity.ModifiedBy = 2;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
