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
using System.Web.Mvc;

namespace OSL.Inventory.B2.Service
{
    public interface IPurchaseService
    {
        Task<bool> CreatePurchaseServiceAsync(PurchaseDto entityDtoToCreate);
        Task<PurchaseDto> GetPurchaseByIdServiceAsync(long? entityDtoToGetId);
        Task<(IEnumerable<PurchaseDatatableViewDto>, int, int)> ListPurchasesWithSortingFilteringPagingServiceAsync(int start, int length, string order, string orderDir,
            string searchByPurchaseCode, DateTime? dateFrom, DateTime? dateTo, string filterBySupplier, StatusDto filterByStatusDto = 0);
        Task<IEnumerable<SupplierDto>> ListSuppliersIdNameServiceAsync();
        Task<IEnumerable<SelectListItem>> SelectListProductsServiceAsync();
        Task<IEnumerable<SelectListItem>> SelectListSuppliersServiceAsync();
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

        public async Task<IEnumerable<SupplierDto>> ListSuppliersIdNameServiceAsync()
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SelectListItem>> SelectListSuppliersServiceAsync()
        {
            try
            {
                var entitiesList = await _unitOfWork.SupplierRepository.ListSuppliersIdNameAsync();

                return (from entity in entitiesList
                        select new SelectListItem()
                        {
                            Value = entity.Id.ToString(),
                            Text = $"{entity.FirstName} {entity.LastName}",
                        }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SelectListItem>> SelectListProductsServiceAsync()
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<(IEnumerable<PurchaseDatatableViewDto>, int, int)> ListPurchasesWithSortingFilteringPagingServiceAsync(int start, int length,
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

                return (listPurchasesDto, totalRecord, filterRecord);
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
