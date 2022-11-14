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
        Task<IEnumerable<Product>> ListProductsIdNameAsync();
        Task<(IEnumerable<Category>, bool)> ListCategoriesAsync(string name, int page
            , int resultCount);
        decimal GetProductUnitPrice(long id);
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

        public decimal GetProductUnitPrice(long id)
        {
            try
            {
                var unitPrice = _context.Products.Single(x => x.Id == id).PricePerUnit;
                return unitPrice;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region ListInstance

        public async Task<(IEnumerable<Category>, bool)> ListCategoriesAsync(string name, int page
            , int resultCount)
        {
            try
            {
                int offset = (page - 1) * resultCount;

                int entitiesCount = await _context.Categories.Where(x =>
                                        (string.IsNullOrEmpty(name) ||
                                        x.Name.ToLower().Contains(name.ToLower())))
                                    .CountAsync();

                var entities = (await _context.Categories.Where(x =>
                                    (string.IsNullOrEmpty(name) ||
                                    x.Name.ToLower().Contains(name.ToLower())))
                                .OrderByDescending(x => x.Name)
                                .Skip(offset).Take(resultCount)
                                .ToListAsync())
                                .Select(x => new Category()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                });

                int endCount = offset + resultCount;
                bool morePages = endCount < entitiesCount;

                return (entities, morePages);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> ListProductsIdNameAsync()
        {
            try
            {
                var entities = (await _context.Products.ToListAsync())
                        .Select(x => new Product()
                        {
                            Id = x.Id,
                            Name = x.Name,
                        });

                return entities;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Operations

        public override bool CreateEntity(Product entityToCreate)
        {
            try
            {
                var entity = _context.Products.Find(entityToCreate.Id);

                if (entity != null) throw new Exception("Product already exist in the database");

                return base.CreateEntity(entityToCreate);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override bool UpdateEntity(Product entityToUpdate)
        {
            try
            {

                if (_context.Products.Any(x => x.Id != entityToUpdate.Id &&
                    x.Name.ToLower().Equals(entityToUpdate.Name.ToLower())))
                    throw new Exception($"{entityToUpdate.Name} with same name already exists");

                return base.UpdateEntity(entityToUpdate);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
