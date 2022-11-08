using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OSL.Inventory.B2.Service
{
    public interface ISupplierService
    {
        Task<bool> CreateSupplierServiceAsync(SupplierDto entityDtoToCreate);
        Task<bool> DeleteSupplierByIdServiceAsync(long entityDtoToDeleteId);
        Task<SupplierDto> GetSupplierByIdServiceAsync(long? entityDtoToGetId);
        Task<(List<object>, int, int)> ListSuppliersWithSortingFilteringPagingServiceAsync(int start, int length, string order, string orderDir,
            string searchByName, StatusDto filterByStatusDto = 0);
        Task<bool> UpdateSupplierServiceAsync(SupplierDto entityDtoToUpdate);
        IDictionary<string, string> ValidateSupplierDtoService(SupplierDto entityDto);
    }

    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region SingleInstance
        public IDictionary<string, string> ValidateSupplierDtoService(SupplierDto entityDto)
        {
            Guard.AgainstNullParameter(entityDto, nameof(entityDto));

            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (entityDto.FirstName.Trim().Length == 0)
                errors.Add("FirstName", "First Name is required.");
            if (entityDto.LastName.Trim().Length == 0)
                errors.Add("LastName", "Last Name is required.");
            return errors;
        }

        public async Task<SupplierDto> GetSupplierByIdServiceAsync(long? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.SupplierRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = new SupplierDto
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    EmailAddress = entity.EmailAddress,
                    PhoneNumber = entity.PhoneNumber,
                    Country = entity.Country,
                    City = entity.City,
                    State = entity.State,
                    ZipCode = entity.ZipCode,
                    Status = (StatusDto)entity.Status,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy,
                    ModifiedAt = entity.ModifiedAt,
                    ModifiedBy = entity.ModifiedBy,
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
        public async Task<(List<object>, int, int)> ListSuppliersWithSortingFilteringPagingServiceAsync(int start, int length, string order, string orderDir,
            string searchByName, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listSuppliersTuple = await _unitOfWork.SupplierRepository.ListSuppliersWithSortingFilteringPagingAsync(start, length,
                    order, orderDir, searchByName, (Status)filterByStatusDto);

                int totalRecord = listSuppliersTuple.Item2;
                int filterRecord = listSuppliersTuple.Item3;
                var listSuppliersDto = listSuppliersTuple.Item1.ConvertToDto();

                List<object> entitiesList = new List<object>();
                foreach (var item in listSuppliersDto)
                {
                    List<string> dataItems = new List<string>
                    {
                        item.Name,
                        item.EmailAddress,
                        item.PhoneNumber,
                        item.Address,
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
        public async Task<bool> CreateSupplierServiceAsync(SupplierDto entityDtoToCreate)
        {
            try
            {
                var entity = new Supplier
                {
                    FirstName = entityDtoToCreate.FirstName,
                    LastName = entityDtoToCreate.LastName,
                    EmailAddress = entityDtoToCreate.EmailAddress,
                    PhoneNumber = entityDtoToCreate.PhoneNumber,
                    Country = entityDtoToCreate.Country,
                    City = entityDtoToCreate.City,
                    State = entityDtoToCreate.State,
                    ZipCode = entityDtoToCreate.ZipCode,
                    Status = Status.Active,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                };

                if (!_unitOfWork.SupplierRepository.CreateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateSupplierServiceAsync(SupplierDto entityDtoToUpdate)
        {
            try
            {
                var entity = new Supplier
                {
                    Id = entityDtoToUpdate.Id,
                    FirstName = entityDtoToUpdate.FirstName,
                    LastName = entityDtoToUpdate.LastName,
                    EmailAddress = entityDtoToUpdate.EmailAddress,
                    PhoneNumber = entityDtoToUpdate.PhoneNumber,
                    Country = entityDtoToUpdate.Country,
                    City = entityDtoToUpdate.City,
                    State = entityDtoToUpdate.State,
                    ZipCode = entityDtoToUpdate.ZipCode,
                    Status = (Status)entityDtoToUpdate.Status,
                    CreatedAt = entityDtoToUpdate.CreatedAt,
                    CreatedBy = entityDtoToUpdate.CreatedBy,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = 2,
                };

                if (!_unitOfWork.SupplierRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteSupplierByIdServiceAsync(long entityDtoToDeleteId)
        {
            try
            {
                if (!await _unitOfWork.SupplierRepository.SoftDeleteEntity(entityDtoToDeleteId)) return false;
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
