using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OSL.Inventory.B2.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> SoftDeleteEntity(long id);
        Task<(IEnumerable<Category>, int, int)> ListCategoriesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0);
        IQueryable<Category> ListCategoriesDropdown();
    }

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly InventoryDbContext _context;

        public CategoryRepository(InventoryDbContext context) :
            base(context)
        {
            _context = context;
        }

        public IQueryable<Category> ListCategoriesDropdown() { return _context.Categories; }

        // search by name
        private async Task<(IEnumerable<Category>, int)> SearchCategoriesByName(string name, int start, int length)
        {
            var recordCount = await _context.Categories
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            var entities = (await _context.Categories
                    .Where(d => d.Status != Status.Deleted)
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    });

            return (entities, recordCount);
        }

        // filter by status
        private async Task<(IEnumerable<Category>, int)> FilterCategoriesByStatus(Status status, int start, int length)
        {
            var recordCount = await _context.Categories
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status)
                );

            var entities = (await _context.Categories
                    .Where(d => d.Status != Status.Deleted)
                    .Where(x => x.Status == status)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    });

            return (entities, recordCount);
        }

        // filter by status and name
        private async Task<(IEnumerable<Category>, int)> CategoriesByNameAndStatus(string name, Status status, int start, int length)
        {
            var recordCount = await _context.Categories
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            var entities = (await _context.Categories
                    .Where(d => d.Status != Status.Deleted)
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .Where(x => x.Status == status)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    });

            return (entities, recordCount);
        }

        // list with paging
        private async Task<(IEnumerable<Category>, int)> ListCategoriesWithPaginationAsync(int start, int length)
        {
            // count records exclude deleted
            var recordCount = await _context.Categories.CountAsync(x => x.Status != Status.Deleted);

            var entities = (await _context.Categories
                    .Where(d => d.Status != Status.Deleted)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    });

            return (entities, recordCount);
        }

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

        public async Task<(IEnumerable<Category>, int, int)> ListCategoriesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Categories.CountAsync();
            // filter record counter
            int filterRecord = 0;

            // Initialization.   
            IEnumerable<Category> listEntites = Enumerable.Empty<Category>();

            if (string.IsNullOrEmpty(searchByName) && filterByStatus == 0)
            {
                var listEntitesTuple = await ListCategoriesWithPaginationAsync(start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else if (filterByStatus == 0)
            {
                // search by category name
                var listEntitesTuple = await SearchCategoriesByName(searchByName, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else if (string.IsNullOrEmpty(searchByName))
            {
                // filter by status
                var listEntitesTuple = await FilterCategoriesByStatus(filterByStatus, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else
            {
                var listEntitesTuple = await CategoriesByNameAndStatus(searchByName, filterByStatus, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }

            // Sorting 
            var result = SortByColumnWithOrder(order, orderDir, listEntites);

            return (result, totalRecord, filterRecord);
        }

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
    }
}
