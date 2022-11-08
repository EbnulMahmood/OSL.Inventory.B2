using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data.Entity.Core.Objects;

namespace OSL.Inventory.B2.Repository
{
    public interface IPurchaseRepository : IBaseRepository<Purchase>    
    {
        Task<(IEnumerable<Purchase>, int, int)> ListPurchasesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByPurchaseCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            Status filterByStatus = 0);
    }

    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        private readonly InventoryDbContext _context;
        public PurchaseRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        #region SingleInstance
        #endregion

        #region ListInstance
        #region Sorting
        // sort by order desc
        // private IEnumerable<Purchase> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Purchase> data)
        // {
        //     // Initialization.   
        //     IEnumerable<Purchase> sortedEntities = Enumerable.Empty<Purchase>();

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

        private Purchase SelectPurchase(Purchase purchase)
        {
            return new Purchase()
            {
                Id = purchase.Id,
                PurchaseCode = purchase.PurchaseCode,
                PurchaseAmount = purchase.PurchaseAmount,
                PurchaseDate = purchase.PurchaseDate,
                PurchaseAmountPaid = purchase.PurchaseAmountPaid,
                AmountPaidTime = purchase.AmountPaidTime,
                Status = purchase.Status,
                SupplierId = purchase.SupplierId,
                Supplier = purchase.Supplier,
            };
        }

        public async Task<(IEnumerable<Purchase>, int, int)> ListPurchasesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByPurchaseCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Purchases.CountAsync();

            var recordCount = await _context.Purchases.CountAsync(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.PurchaseCode.ToLower().Contains(searchByPurchaseCode.ToLower()) || string.IsNullOrEmpty(searchByPurchaseCode)) &&
                                                    (DbFunctions.TruncateTime(x.PurchaseDate) >= dateFrom || dateFrom == null) &&
                                                    (DbFunctions.TruncateTime(x.PurchaseDate) <= dateTo || dateTo == null) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0));

            IEnumerable<Purchase> listEntites = (await _context.Purchases
                                                .Where(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.PurchaseCode.ToLower().Contains(searchByPurchaseCode.ToLower()) || string.IsNullOrEmpty(searchByPurchaseCode)) &&
                                                    (DbFunctions.TruncateTime(x.PurchaseDate) >= dateFrom || dateFrom == null) &&
                                                    (DbFunctions.TruncateTime(x.PurchaseDate) <= dateTo || dateTo == null) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0))
                                                .OrderByDescending(d => d.CreatedAt)
                                                .Skip(start).Take(length)
                                                .ToListAsync())
                                                .Select(purchase => SelectPurchase(purchase));

            // Sorting 
            // var result = SortByColumnWithOrder(order, orderDir, listEntites);

            // return (result, totalRecord, recordCount);
            return (listEntites, totalRecord, recordCount);
        }
        #endregion

        #region Operations
        #endregion
    }
}
