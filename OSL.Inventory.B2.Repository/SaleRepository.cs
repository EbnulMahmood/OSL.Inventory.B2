using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data.Entity;

namespace OSL.Inventory.B2.Repository
{
    public interface ISaleRepository
    {
        Task<(IEnumerable<Sale>, int, int)> ListSalesWithSortingFilteringPagingAsync(int start, int length, string order, string orderDir, 
            string searchBySaleCode, DateTime? dateFrom, DateTime? dateTo, Status filterByStatus = 0);
    }

    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private readonly InventoryDbContext _context;
        public SaleRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        #region SingleInstance
        #endregion

        #region ListInstance
        #region Sorting
        // sort by order desc
        private IEnumerable<Sale> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Sale> data)
        {
            // Initialization.   
            IEnumerable<Sale> sortedEntities = Enumerable.Empty<Sale>();

            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.SaleCode)
                            .ToList()
                            .Select(sale => SelectSale(sale)) :
                            data.OrderBy(p => p.SaleCode)
                            .ToList()
                            .Select(sale => SelectSale(sale));
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.SaleAmount)
                            .ToList()
                            .Select(sale => SelectSale(sale)) :
                            data.OrderBy(p => p.SaleAmount)
                            .ToList()
                            .Select(sale => SelectSale(sale));
                        break;
                    case "2":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.SaleDate)
                            .ToList()
                            .Select(sale => SelectSale(sale)) :
                            data.OrderBy(p => p.SaleDate)
                            .ToList()
                            .Select(sale => SelectSale(sale));
                        break;
                    case "3":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.SaleAmountPaid)
                            .ToList()
                            .Select(sale => SelectSale(sale)) :
                            data.OrderBy(p => p.SaleAmountPaid)
                            .ToList()
                            .Select(sale => SelectSale(sale));
                        break;
                    case "4":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.AmountPaidTime)
                            .ToList()
                            .Select(sale => SelectSale(sale)) :
                            data.OrderBy(p => p.AmountPaidTime)
                            .ToList()
                            .Select(sale => SelectSale(sale));
                        break;
                    case "5":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Status)
                            .ToList()
                            .Select(sale => SelectSale(sale)) :
                            data.OrderBy(p => p.Status)
                            .ToList()
                            .Select(sale => SelectSale(sale));
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.CreatedAt)
                            .ToList()
                            .Select(sale => SelectSale(sale)) :
                            data.OrderBy(p => p.CreatedAt)
                            .ToList()
                            .Select(sale => SelectSale(sale));
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

        private Sale SelectSale(Sale sale)
        {
            return new Sale()
            {
                Id = sale.Id,
                SaleCode = sale.SaleCode,
                SaleAmount = sale.SaleAmount,
                SaleDate = sale.SaleDate,
                SaleAmountPaid = sale.SaleAmountPaid,
                AmountPaidTime = sale.AmountPaidTime,
                Status = sale.Status,
                CustomerId = sale.CustomerId,
                Customer = sale.Customer,
            };
        }

        public async Task<(IEnumerable<Sale>, int, int)> ListSalesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchBySaleCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Sales.CountAsync();

            var recordCount = await _context.Sales.CountAsync(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.SaleCode.ToLower().Contains(searchBySaleCode.ToLower()) || string.IsNullOrEmpty(searchBySaleCode)) &&
                                                    (DbFunctions.TruncateTime(x.SaleDate) >= dateFrom || dateFrom == null) &&
                                                    (DbFunctions.TruncateTime(x.SaleDate) <= dateTo || dateTo == null) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0));

            IEnumerable<Sale> listEntites = (await _context.Sales
                                                .Include(x => x.Customer)
                                                .Where(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.SaleCode.ToLower().Contains(searchBySaleCode.ToLower()) || string.IsNullOrEmpty(searchBySaleCode)) &&
                                                    (DbFunctions.TruncateTime(x.SaleDate) >= dateFrom || dateFrom == null) &&
                                                    (DbFunctions.TruncateTime(x.SaleDate) <= dateTo || dateTo == null) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0))
                                                .OrderByDescending(d => d.CreatedAt)
                                                .Skip(start).Take(length)
                                                .ToListAsync())
                                                .Select(sale => SelectSale(sale));

            // Sorting 
            var result = SortByColumnWithOrder(order, orderDir, listEntites);

            return (result, totalRecord, recordCount);
        }
        #endregion

        #region Operations
        #endregion
    }
}
