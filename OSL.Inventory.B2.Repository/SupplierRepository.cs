using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OSL.Inventory.B2.Repository
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task<(IEnumerable<Supplier>, int, int)> ListSuppliersWithSortingFilteringPagingAsync(int start, int length, string order, string orderDir,
            string searchByName, Status filterByStatus = 0);
        Task<bool> SoftDeleteEntity(long id);
    }

    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly InventoryDbContext _context;

        public SupplierRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        #region SingleInstance
        #endregion

        #region ListInstance

        #region Sorting
        // sort by order desc
        // private IEnumerable<Supplier> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Supplier> data)
        // {
        //     // Initialization.   
        //     IEnumerable<Supplier> sortedEntities = Enumerable.Empty<Supplier>();

        //     try
        //     {
        //         // Sorting   
        //         switch (order)
        //         {
        //             case "0":
        //                 // Setting.   
        //                 sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
        //                     data.OrderByDescending(p => p.Name)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product)) :
        //                     data.OrderBy(p => p.Name)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product));
        //                 break;
        //             case "1":
        //                 // Setting.   
        //                 sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
        //                     data.OrderByDescending(p => p.InStock)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product)) :
        //                     data.OrderBy(p => p.InStock)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product));
        //                 break;
        //             case "2":
        //                 // Setting.   
        //                 sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
        //                     data.OrderByDescending(p => p.PricePerUnit)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product)) :
        //                     data.OrderBy(p => p.PricePerUnit)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product));
        //                 break;
        //             case "3":
        //                 // Setting.   
        //                 sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
        //                     data.OrderByDescending(p => p.Status)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product)) :
        //                     data.OrderBy(p => p.Status)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product));
        //                 break;
        //             default:
        //                 // Setting.   
        //                 sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
        //                     data.OrderByDescending(p => p.CreatedAt)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product)) :
        //                     data.OrderBy(p => p.CreatedAt)
        //                     .ToList()
        //                     .Select(product => SelectProduct(product));
        //                 break;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // info.   
        //         Console.Write(ex);
        //     }
        //     // info.   
        //     return sortedEntities;
        // }
        #endregion

        private Supplier SelectSupplier(Supplier supplier)
        {
            return new Supplier()
            {
                Id = supplier.Id,
                FirstName = supplier.FirstName,
                LastName = supplier.LastName,
                EmailAddress = supplier.EmailAddress,
                PhoneNumber = supplier.PhoneNumber,
                Country = supplier.Country,
                City = supplier.City,
                State = supplier.State,
                ZipCode = supplier.ZipCode,
            };
        }

        public async Task<(IEnumerable<Supplier>, int, int)> ListSuppliersWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Suppliers.CountAsync();

            var recordCount = await _context.Suppliers.CountAsync(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.FirstName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.LastName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0));

            IEnumerable<Supplier> listEntites = (await _context.Suppliers
                                                .Where(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.FirstName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.LastName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0))
                                                .OrderByDescending(d => d.CreatedAt)
                                                .Skip(start).Take(length)
                                                .ToListAsync())
                                                .Select(supplier => SelectSupplier(supplier));

            // Sorting 
            // var result = SortByColumnWithOrder(order, orderDir, listEntites);

            // return (result, totalRecord, recordCount);
            return (listEntites, totalRecord, recordCount);
        }
        #endregion

        #region Operations
        public async Task<bool> SoftDeleteEntity(long id)
        {
            try
            {
                var entity = await _context.Suppliers.FindAsync(id) ??
                    throw new Exception("Supplier does not exist in the database");

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
