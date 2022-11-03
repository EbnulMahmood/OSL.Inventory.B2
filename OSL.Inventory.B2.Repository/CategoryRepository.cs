using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly InventoryDbContext _context;

        public CategoryRepository(InventoryDbContext context) :
            base(context)
        {
            _context = context;
        }

        // search by name
        private async Task<IEnumerable<Category>> SearchCategoriesByName(string name)
        {
            return await _context.Categories
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .Where(x => x.Status != Status.Deleted)
                    .ToListAsync();
        }

        // filter by status
        private async Task<IEnumerable<Category>> FilterCategoriesByStatus(Status status)
        {
            return await _context.Categories.Where(x => x.Status == status)
                    .Where(x => x.Status != Status.Deleted)
                    .ToListAsync();
        }

        // list with paging
        private async Task<(IEnumerable<Category>, int)> ListCategoriesWithPaginationAsync(int start, int length)
        {
            // count records exclude deleted
            var recordCount = await _context.Categories.CountAsync(x => x.Status != Status.Deleted);
            
            var categories = await _context.Categories.Where(d => d.Status != Status.Deleted)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync();
            
            return (categories, recordCount);
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
                            data.OrderByDescending(p => p.Name).ToList() : data.OrderBy(p => p.Name).ToList();
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.Status).ToList() : 
                            data.OrderBy(p => p.Status).ToList();
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.CreatedAt).ToList() : 
                            data.OrderBy(p => p.CreatedAt).ToList();
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
            IEnumerable<Category> listCategories = Enumerable.Empty<Category>();
            bool isFiltered = false;

            // search by category name
            if (!string.IsNullOrEmpty(searchByName))
            {
                listCategories = await SearchCategoriesByName(searchByName);
                isFiltered = true;
            }

            // filter by status
            if (filterByStatus != 0)
            {
                listCategories = await FilterCategoriesByStatus(filterByStatus);
                isFiltered = true;
            }


            //pagination
            if (!isFiltered)
            {
                var tuple = await ListCategoriesWithPaginationAsync(start, length);

                listCategories = tuple.Item1;

                // get total count of records
                filterRecord = tuple.Item2;
            }
            else
            {
                // get total count of records after searching, sorting and filtering
                filterRecord = listCategories.Count();

                listCategories = listCategories.Where(x => x.Status != Status.Deleted)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToList();
            }

            // Sorting 
            var result = SortByColumnWithOrder(order, orderDir, listCategories);

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
