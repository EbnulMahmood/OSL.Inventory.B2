using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace OSL.Inventory.B2.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<(IEnumerable<Product>, int, int)> ListProductsWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, string filterByCategory, Status filterByStatus = 0);
        Task<bool> SoftDeleteEntity(long id);
    }

    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        private Product SelectProduct(Product product)
        {
            return new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Status = product.Status,
                InStock = product.InStock,
                PricePerUnit = product.PricePerUnit,
                BasicUnit = product.BasicUnit,
                Category = product.Category,
            };
        }

        // search by name
        private async Task<(IEnumerable<Product>, int)> SearchProductsByName(string name, int start, int length)
        {
            var recordCount = await _context.Products
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            var entities = (await _context.Products
                    .Include(p => p.Category)
                    .Where(d => d.Status != Status.Deleted)
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(product => SelectProduct(product));

            return (entities, recordCount);
        }

        // filter by status
        private async Task<(IEnumerable<Product>, int)> FilterProductsByStatus(Status status, int start, int length)
        {
            var recordCount = await _context.Products
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status)
                );

            var entities = (await _context.Products
                    .Include(p => p.Category)
                    .Where(d => d.Status != Status.Deleted)
                    .Where(x => x.Status == status)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(product => SelectProduct(product));

            return (entities, recordCount);
        }

        // filter by category
        private async Task<(IEnumerable<Product>, int)> FilterProductsByCategory(long categoryId, int start, int length)
        {
            var recordCount = await _context.Products
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.CategoryId == categoryId)
                );

            var entities = (await _context.Products
                    .Include(p => p.Category)
                    .Where(d => d.Status != Status.Deleted)
                    .Where(x => x.Category.Id == categoryId)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(product => SelectProduct(product));

            return (entities, recordCount);
        }

        // filter by status and name
        private async Task<(IEnumerable<Product>, int)> ProductsByNameAndStatus(string name, Status status, int start, int length)
        {
            var recordCount = await _context.Products
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            var entities = (await _context.Products
                    .Include(p => p.Category)
                    .Where(d => d.Status != Status.Deleted)
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .Where(x => x.Status == status)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(product => SelectProduct(product));

            return (entities, recordCount);
        }

        // list with paging
        private async Task<(IEnumerable<Product>, int)> ListProductsWithPaginationAsync(int start, int length)
        {
            // count records exclude deleted
            var recordCount = await _context.Products.CountAsync(x => x.Status != Status.Deleted);

            var entities = (await _context.Products
                    .Include(p => p.Category)
                    .Where(d => d.Status != Status.Deleted)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(product => SelectProduct(product));

            return (entities, recordCount);
        }

        #region Sorting
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
                            data.OrderByDescending(p => p.Name)
                            .ToList()
                            .Select(product => SelectProduct(product)) :
                            data.OrderBy(p => p.Name)
                            .ToList()
                            .Select(product => SelectProduct(product));
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.InStock)
                            .ToList()
                            .Select(product => SelectProduct(product)) :
                            data.OrderBy(p => p.InStock)
                            .ToList()
                            .Select(product => SelectProduct(product));
                        break;
                    case "2":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.PricePerUnit)
                            .ToList()
                            .Select(product => SelectProduct(product)) :
                            data.OrderBy(p => p.PricePerUnit)
                            .ToList()
                            .Select(product => SelectProduct(product));
                        break;
                    case "3":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Status)
                            .ToList()
                            .Select(product => SelectProduct(product)) :
                            data.OrderBy(p => p.Status)
                            .ToList()
                            .Select(product => SelectProduct(product));
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.CreatedAt)
                            .ToList()
                            .Select(product => SelectProduct(product)) :
                            data.OrderBy(p => p.CreatedAt)
                            .ToList()
                            .Select(product => SelectProduct(product));
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

        public async Task<(IEnumerable<Product>, int, int)> ListProductsWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, string filterByCategory, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Products.CountAsync();
            // filter record counter
            int filterRecord = 0;
            long categoryId = 0;
            if (!string.IsNullOrEmpty(filterByCategory))
            {
                categoryId = long.Parse(filterByCategory);
            }

            // Initialization.   
            IEnumerable<Product> listEntites = Enumerable.Empty<Product>();

            if (string.IsNullOrEmpty(searchByName) && filterByStatus == 0 && categoryId == 0)
            {
                var listEntitesTuple = await ListProductsWithPaginationAsync(start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else if (filterByStatus == 0 && categoryId == 0)
            {
                // search by Product name
                var listEntitesTuple = await SearchProductsByName(searchByName, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else if (filterByStatus == 0)
            {
                // search by Product category
                var listEntitesTuple = await FilterProductsByCategory(categoryId, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else if (string.IsNullOrEmpty(searchByName))
            {
                // filter by status
                var listEntitesTuple = await FilterProductsByStatus(filterByStatus, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else
            {
                var listEntitesTuple = await ProductsByNameAndStatus(searchByName, filterByStatus, start, length);
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
