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
        private IEnumerable<Purchase> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Purchase> data)
        {
            // Initialization.   
            IEnumerable<Purchase> sortedEntities = Enumerable.Empty<Purchase>();

            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.PurchaseCode)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase)) :
                            data.OrderBy(p => p.PurchaseCode)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase));
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.PurchaseAmount)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase)) :
                            data.OrderBy(p => p.PurchaseAmount)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase));
                        break;
                    case "2":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.PurchaseDate)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase)) :
                            data.OrderBy(p => p.PurchaseDate)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase));
                        break;
                    case "3":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.PurchaseAmountPaid)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase)) :
                            data.OrderBy(p => p.PurchaseAmountPaid)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase));
                        break;
                    case "4":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.AmountPaidTime)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase)) :
                            data.OrderBy(p => p.AmountPaidTime)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase));
                        break;
                    case "5":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Status)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase)) :
                            data.OrderBy(p => p.Status)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase));
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.CreatedAt)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase)) :
                            data.OrderBy(p => p.CreatedAt)
                            .ToList()
                            .Select(purchase => SelectPurchase(purchase));
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
                                                .Include(x => x.Supplier)
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
            var result = SortByColumnWithOrder(order, orderDir, listEntites);

            return (result, totalRecord, recordCount);
        }
        #endregion

        #region Operations
        #endregion
    }
}
