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
    public interface ICustomerService
    {
        Task<bool> CreateCustomerServiceAsync(CustomerDto entityDtoToCreate);
        Task<bool> DeleteCustomerByIdServiceAsync(long entityDtoToDeleteId);
        Task<CustomerDto> GetCustomerByIdServiceAsync(long? entityDtoToGetId);
        Task<(List<object>, int, int)> ListCustomersWithSortingFilteringPagingServiceAsync(int start, int length, string order, string orderDir,
            string searchByName, StatusDto filterByStatusDto = 0);
        Task<bool> UpdateCustomerServiceAsync(CustomerDto entityDtoToUpdate);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region SingleInstance

        public async Task<CustomerDto> GetCustomerByIdServiceAsync(long? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.CustomerRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = new CustomerDto
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
        public async Task<(List<object>, int, int)> ListCustomersWithSortingFilteringPagingServiceAsync(int start, int length, string order, string orderDir,
            string searchByName, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listCustomersTuple = await _unitOfWork.CustomerRepository.ListCustomersWithSortingFilteringPagingAsync(start, length,
                    order, orderDir, searchByName, (Status)filterByStatusDto);

                int totalRecord = listCustomersTuple.Item2;
                int filterRecord = listCustomersTuple.Item3;
                var listCustomersDto = listCustomersTuple.Item1.ConvertToDto();

                List<object> entitiesList = new List<object>();
                foreach (var item in listCustomersDto)
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
        public async Task<bool> CreateCustomerServiceAsync(CustomerDto entityDtoToCreate)
        {
            try
            {
                var entity = new Customer
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

                if (!_unitOfWork.CustomerRepository.CreateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateCustomerServiceAsync(CustomerDto entityDtoToUpdate)
        {
            try
            {
                var entity = new Customer
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

                if (!_unitOfWork.CustomerRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCustomerByIdServiceAsync(long entityDtoToDeleteId)
        {
            try
            {
                if (!await _unitOfWork.CustomerRepository.SoftDeleteEntity(entityDtoToDeleteId)) return false;
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
