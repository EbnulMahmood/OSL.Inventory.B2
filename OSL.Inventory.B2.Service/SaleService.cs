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
using System.Web.Mvc;
using Autofac.Core;

namespace OSL.Inventory.B2.Service
{
    public interface ISaleService
    {
        Task<bool> CreateSaleServiceAsync(SaleDto entityDtoToCreate);
        Task<SaleDto> GetSaleByIdServiceAsync(long? entityDtoToGetId);
        decimal GetProductUnitPriceService(long id);
        Task<IEnumerable<CustomerDto>> SelectCustomerListItemsAsync();
        Task<IEnumerable<SelectListItem>> SelectListCustomersServiceAsync();
        Task<IEnumerable<SelectListItem>> SelectListProductsServiceAsync();
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

        public decimal GetProductUnitPriceService(long id)
        {
            try
            {
                var unitPrice = _unitOfWork.ProductRepository.GetProductUnitPrice(id);
                return unitPrice;
            }
            catch (Exception)
            {

                throw;
            }
        }

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

        public async Task<IEnumerable<SelectListItem>> SelectListCustomersServiceAsync()
        {
            var entitiesList = await _unitOfWork.CustomerRepository.ListCustomersIdNameAsync();

            return (from entity in entitiesList
                    select new SelectListItem()
                    {
                        Value = entity.Id.ToString(),
                        Text = $"{entity.FirstName} {entity.LastName}",
                    }).ToList();
        }

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

        public async Task<IEnumerable<SelectListItem>> SelectListProductsServiceAsync()
        {
            var entities = await _unitOfWork.ProductRepository.ListProductsIdNameAsync();
            var selectListItems = (from x in entities
                                   select new SelectListItem()
                                   {
                                       Text = x.Name,
                                       Value = x.Id.ToString(),
                                       Selected = false,
                                   }).ToList();
            return selectListItems;
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
                        item.AmountPaidTime?.ToUniversalTime().Humanize(),
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
                var saleDetails = (from x in entityDtoToCreate.SaleDetailsDto
                                  select new SaleDetail()
                                  {
                                      QuantitySold = x.QuantitySold,
                                      PricePerUnit = x.PricePerUnit,
                                      TotalPrice = x.TotalPrice,
                                      ProductId = x.ProductId,
                                  }).ToList();

                var entity = new Sale
                {
                    SaleCode = entityDtoToCreate.SaleCode,
                    SaleAmount = entityDtoToCreate.SaleAmount,
                    SaleDate = DateTime.Now,
                    CustomerId = entityDtoToCreate.CustomerId,
                    SaleDetails = saleDetails,
                    Status = Status.Active,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                };

                _unitOfWork.SaleRepository.CreateEntity(entity);

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
