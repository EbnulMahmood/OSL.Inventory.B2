using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OSL.Inventory.B2.Service.Extensions;
using Humanizer;
using System.Linq;

namespace OSL.Inventory.B2.Service
{
    public interface ISaleService
    {
        Task<bool> CreateSaleServiceAsync(SaleDto entityDtoToCreate);
        Task<SaleDto> GetSaleByIdServiceAsync(long? entityDtoToGetId);
        Task<IEnumerable<CustomerDto>> SelectCustomerListItemsAsync();
        Task<(List<object>, int, int)> ListSalesWithSortingFilteringPagingServiceAsync(int start, int length, string order, string orderDir,
            string searchBySaleCode, DateTime? dateFrom, DateTime? dateTo, string filterByCustomer, StatusDto filterByStatusDto = 0);
        Task<bool> UpdateSaleServiceAsync(SaleDto entityDtoToUpdate);
    }

    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region SingleInstance
        public async Task<SaleDto> GetSaleByIdServiceAsync(long? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.SaleRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = new SaleDto
                {
                    Id = entity.Id,
                    SaleCode = entity.SaleCode,
                    SaleAmount = entity.SaleAmount,
                    SaleDate = entity.SaleDate,
                    SaleAmountPaid = entity.SaleAmountPaid,
                    AmountPaidTime = entity.AmountPaidTime,
                    CustomerId = entity.CustomerId,
                };
                return entityDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region ListInstance

        public async Task<IEnumerable<CustomerDto>> SelectCustomerListItemsAsync()
        {
            var entities = await _unitOfWork.CustomerRepository.ListCustomersIdNameAsync();
            var entitiesDto = (from x in entities
                               select new CustomerDto()
                               {
                                   Id = x.Id,
                                   FirstName = x.FirstName,
                               }).ToList();
            return entitiesDto;
        }

        public async Task<(List<object>, int, int)> ListSalesWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchBySaleCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            string filterByCustomer, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listSalesTuple = await _unitOfWork.SaleRepository.ListSalesWithSortingFilteringPagingAsync(start, length,
                    order, orderDir, searchBySaleCode, dateFrom, dateTo, filterByCustomer, (Status)filterByStatusDto);

                int totalRecord = listSalesTuple.Item2;
                int filterRecord = listSalesTuple.Item3;
                var listSalesDto = listSalesTuple.Item1.ConvertToDto();

                List<object> entitiesList = new List<object>();
                foreach (var item in listSalesDto)
                {
                    List<string> dataItems = new List<string>
                    {
                        item.SaleCode,
                        item.SaleAmount.ToString(),
                        item.SaleDate.ToUniversalTime().Humanize(),
                        item.SaleAmountPaid.ToString(),
                        item.AmountPaidTime.ToUniversalTime().Humanize(),
                        item.StatusHtml,
                        item.ActionLinkHtml
                    };

                    entitiesList.Add(dataItems);
                }

                return (entitiesList, totalRecord, filterRecord);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Operations

        public async Task<bool> CreateSaleServiceAsync(SaleDto entityDtoToCreate)
        {
            try
            {
                var entity = new Sale
                {
                    Id = entityDtoToCreate.Id,
                    SaleCode = entityDtoToCreate.SaleCode,
                    SaleAmount = entityDtoToCreate.SaleAmount,
                    SaleDate = entityDtoToCreate.SaleDate,
                    SaleAmountPaid = entityDtoToCreate.SaleAmountPaid,
                    AmountPaidTime = entityDtoToCreate.AmountPaidTime,
                    CustomerId = entityDtoToCreate.CustomerId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                };

                if (!_unitOfWork.SaleRepository.CreateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateSaleServiceAsync(SaleDto entityDtoToUpdate)
        {
            try
            {
                var entity = new Sale
                {
                    Id = entityDtoToUpdate.Id,
                    SaleCode = entityDtoToUpdate.SaleCode,
                    SaleAmount = entityDtoToUpdate.SaleAmount,
                    SaleDate = entityDtoToUpdate.SaleDate,
                    SaleAmountPaid = entityDtoToUpdate.SaleAmountPaid,
                    AmountPaidTime = entityDtoToUpdate.AmountPaidTime,
                    CustomerId = entityDtoToUpdate.CustomerId,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = 2
                };

                if (!_unitOfWork.SaleRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
