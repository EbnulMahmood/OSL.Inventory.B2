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
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<(IEnumerable<Customer>, int, int)> ListCustomersWithSortingFilteringPagingAsync(int start, int length, string order, string orderDir, 
            string searchByName, Status filterByStatus = 0);
        Task<bool> SoftDeleteEntity(long id);
    }

    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly InventoryDbContext _context;

        public CustomerRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        #region SingleInstance
        #endregion

        #region ListInstance

        #region Sorting
        // sort by order desc
        // private IEnumerable<Customer> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Customer> data)
        // {
        //     // Initialization.   
        //     IEnumerable<Customer> sortedEntities = Enumerable.Empty<Customer>();

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

        private Customer SelectCustomer(Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress,
                PhoneNumber = customer.PhoneNumber,
                Country = customer.Country,
                City = customer.City,
                State = customer.State,
                ZipCode = customer.ZipCode,
            };
        }

        public async Task<(IEnumerable<Customer>, int, int)> ListCustomersWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Customers.CountAsync();

            var recordCount = await _context.Customers.CountAsync(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.FirstName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.LastName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0));

            IEnumerable<Customer> listEntites = (await _context.Customers
                                                .Where(x =>
                                                    (x.Status != Status.Deleted) &&
                                                    (x.FirstName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.LastName.ToLower().Contains(searchByName.ToLower()) || string.IsNullOrEmpty(searchByName)) &&
                                                    (x.Status == filterByStatus || filterByStatus == 0))
                                                .OrderByDescending(d => d.CreatedAt)
                                                .Skip(start).Take(length)
                                                .ToListAsync())
                                                .Select(customer => SelectCustomer(customer));

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
                var entity = await _context.Customers.FindAsync(id) ??
                    throw new Exception("Customer does not exist in the database");

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
