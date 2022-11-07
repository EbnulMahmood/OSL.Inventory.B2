using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using OSL.Inventory.B2.Entity;

namespace OSL.Inventory.B2.Service
{
    public interface IPurchaseService
    {

    }

    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region SingleInstance
        #endregion

        #region ListInstance
        #endregion

        #region Operations
        #endregion

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
                };
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = 1;

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
                };
                entity.ModifiedAt = DateTime.Now;
                entity.ModifiedBy = 2;

                if (!_unitOfWork.PurchaseRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
