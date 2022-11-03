using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;
using OSL.Inventory.B2.Repository.Interfaces;

namespace OSL.Inventory.B2.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        // search by name
        private async Task<IEnumerable<Product>> SearchProductsByName(string name)
        {
            return await _context.Products
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .Where(x => x.Status != Status.Deleted)
                    .ToListAsync();
        }

        // filter by status
        private async Task<IEnumerable<Product>> FilterProductsByStatus(Status status)
        {
            return await _context.Products.Where(x => x.Status == status)
                    .Where(x => x.Status != Status.Deleted)
                    .ToListAsync();
        }

        // list with paging
        private async Task<(IEnumerable<Product>, int)> ListProductsWithPaginationAsync(int start, int length)
        {
            // count records exclude deleted
            var recordCount = await _context.Products.CountAsync(x => x.Status != Status.Deleted);

            var products = await _context.Products.Where(d => d.Status != Status.Deleted)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync();

            return (products, recordCount);
        }

        // sort by order desc
        private IEnumerable<Product> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Product> data)
        {
            // Initialization.   
            IEnumerable<Product> sortedEntities = Enumerable.Empty<Product>();
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

        public async Task<(IEnumerable<Product>, int, int)> ListProductsWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Products.CountAsync();
            // filter record counter
            int filterRecord = 0;

            // Initialization.   
            IEnumerable<Product> listProducts = Enumerable.Empty<Product>();
            bool isFiltered = false;

            // search by Product name
            if (!string.IsNullOrEmpty(searchByName))
            {
                listProducts = await SearchProductsByName(searchByName);
                isFiltered = true;
            }

            // filter by status
            if (filterByStatus != 0)
            {
                listProducts = await FilterProductsByStatus(filterByStatus);
                isFiltered = true;
            }

            //pagination
            if (!isFiltered)
            {
                var tuple = await ListProductsWithPaginationAsync(start, length);

                listProducts = tuple.Item1;

                // get total count of records
                filterRecord = tuple.Item2;
            }
            else
            {
                // get total count of records after searching, sorting and filtering
                filterRecord = listProducts.Count();

                listProducts = listProducts.Where(x => x.Status != Status.Deleted)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToList();
            }

            // Sorting 
            var result = SortByColumnWithOrder(order, orderDir, listProducts);

            return (result, totalRecord, filterRecord);
        }

        public async Task<bool> SoftDeleteEntity(long id)
        {
            try
            {
                var entity = await _context.Products.FindAsync(id) ??
                    throw new Exception("Product does not exist in the database");

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
