using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Xml.Linq;

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

        #region SingleInstance
        #endregion

        #region ListInstance

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

        public async Task<(IEnumerable<Product>, int, int)> ListProductsWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, string filterByCategory, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Products.CountAsync();

            long categoryId = 0;
            if (!string.IsNullOrEmpty(filterByCategory))
            {
                categoryId = long.Parse(filterByCategory);
            }

            var recordCount = await _context.Products.CountAsync(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.Name.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0) &&
                                                    (x.CategoryId == categoryId || categoryId == 0));

            IEnumerable<Product> listEntites = (await _context.Products
                                                .Include(p => p.Category)
                                                .Where(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.Name.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0) &&
                                                    (x.CategoryId == categoryId || categoryId == 0))
                                                .OrderByDescending(d => d.CreatedAt)
                                                .Skip(start).Take(length)
                                                .ToListAsync())
                                                .Select(product => SelectProduct(product));

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
        #endregion
    }
}
