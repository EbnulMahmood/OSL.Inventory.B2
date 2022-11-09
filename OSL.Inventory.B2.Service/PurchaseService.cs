using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Service.DTOs;
using System.Threading.Tasks;
using System;
using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using System.Collections.Generic;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service.Extensions;
using Humanizer;
using System.Linq;

namespace OSL.Inventory.B2.Service
{
    public interface IPurchaseService
    {
        Task<(List<object>, int, int)> ListPurchasesWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchByPurchaseCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            string filterBySupplier, StatusDto filterByStatusDto = 0);
        Task<IEnumerable<SupplierDto>> SelectSupplierListItemsAsync();
        Task<bool> CreatePurchaseServiceAsync(PurchaseDto entityDtoToCreate);
        Task<PurchaseDto> GetPurchaseByIdServiceAsync(long? entityDtoToGetId);
        Task<bool> UpdatePurchaseServiceAsync(PurchaseDto entityDtoToUpdate);
    }

    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region SingleInstance
        public async Task<PurchaseDto> GetPurchaseByIdServiceAsync(long? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.PurchaseRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = new PurchaseDto
                {
                    Id = entity.Id,
                    PurchaseCode = entity.PurchaseCode,
                    PurchaseAmount = entity.PurchaseAmount,
                    PurchaseDate = entity.PurchaseDate,
                    PurchaseAmountPaid = entity.PurchaseAmountPaid,
                    AmountPaidTime = entity.AmountPaidTime,
                    SupplierId = entity.SupplierId,
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

        public async Task<IEnumerable<SupplierDto>> SelectSupplierListItemsAsync()
        {
            var entities = await _unitOfWork.SupplierRepository.ListSuppliersIdNameAsync();
            var entitiesDto = (from x in entities
                               select new SupplierDto()
                               {
                                   Id = x.Id,
                                   FirstName = x.FirstName,
                               }).ToList();
            return entitiesDto;
        }
        
        public async Task<(List<object>, int, int)> ListPurchasesWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchByPurchaseCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            string filterBySupplier, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listPurchasesTuple = await _unitOfWork.PurchaseRepository.ListPurchasesWithSortingFilteringPagingAsync(start, length,
                    order, orderDir, searchByPurchaseCode, dateFrom, dateTo, filterBySupplier, (Status)filterByStatusDto);

                int totalRecord = listPurchasesTuple.Item2;
                int filterRecord = listPurchasesTuple.Item3;
                var listPurchasesDto = listPurchasesTuple.Item1.ConvertToDto();

                List<object> entitiesList = new List<object>();
                foreach (var item in listPurchasesDto)
                {
                    List<string> dataItems = new List<string>
                    {
                        item.PurchaseCode,
                        item.PurchaseAmount.ToString(),
                        item.PurchaseDate.ToUniversalTime().Humanize(),
                        item.PurchaseAmountPaid.ToString(),
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

        public async Task<bool> CreatePurchaseServiceAsync(PurchaseDto entityDtoToCreate)
        {
            try
            {
                var entity = new Purchase
                {
                    Id = entityDtoToCreate.Id,
                    PurchaseCode = entityDtoToCreate.PurchaseCode,
                    PurchaseAmount = entityDtoToCreate.PurchaseAmount,
                    PurchaseDate = entityDtoToCreate.PurchaseDate,
                    PurchaseAmountPaid = entityDtoToCreate.PurchaseAmountPaid,
                    AmountPaidTime = entityDtoToCreate.AmountPaidTime,
                    SupplierId = entityDtoToCreate.SupplierId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                };

                if (!_unitOfWork.PurchaseRepository.CreateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdatePurchaseServiceAsync(PurchaseDto entityDtoToUpdate)
        {
            try
            {
                var entity = new Purchase
                {
                    Id = entityDtoToUpdate.Id,
                    PurchaseCode = entityDtoToUpdate.PurchaseCode,
                    PurchaseAmount = entityDtoToUpdate.PurchaseAmount,
                    PurchaseDate = entityDtoToUpdate.PurchaseDate,
                    PurchaseAmountPaid = entityDtoToUpdate.PurchaseAmountPaid,
                    AmountPaidTime = entityDtoToUpdate.AmountPaidTime,
                    SupplierId = entityDtoToUpdate.SupplierId,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = 2
                };

                if (!_unitOfWork.PurchaseRepository.UpdateEntity(entity)) throw new Exception();

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
