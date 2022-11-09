using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> ListUsersAsync()
        {
            try
            {
                var entities = await _unitOfWork.UserRepository.ListEntitiesAsync();
                var entitiesDto = (from user in entities
                                   select new UserDto()
                                   {
                                       Id = user.Id,
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       Country = user.Country,
                                       City = user.City,
                                       State = user.State,
                                       ZipCode = user.ZipCode,
                                       CreatedAt = user.CreatedAt,
                                       CreatedBy = user.CreatedBy,
                                       ModifiedAt = user.ModifiedAt,
                                       ModifiedBy = user.ModifiedBy,
                                       IdentityUserId = user.IdentityUserId,
                                   }).ToList();
                return entitiesDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDto> GetUserByIdAsync(long id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
                var entityDto = new UserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Country = user.Country,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedAt = user.CreatedAt,
                    CreatedBy = user.CreatedBy,
                    ModifiedAt = user.ModifiedAt,
                    ModifiedBy = user.ModifiedBy,
                    IdentityUserId = user.IdentityUserId,
                };
                return entityDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CreateUserAsync(string FirstName, string LastName,
                        string Country, string City, string State, string ZipCode, long IdentityUserId)
        {
            try
            {
                var entity = new InventoryUser
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Country = Country,
                    City = City,
                    State = State,
                    ZipCode = ZipCode,
                    CreatedAt = DateTime.Now,
                    IdentityUserId = IdentityUserId,
                };

                if (!_unitOfWork.UserRepository.CreateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(UserDto entityDtoToUpdate)
        {
            try
            {
                var entity = new InventoryUser()
                {
                    Id = entityDtoToUpdate.Id,
                    FirstName = entityDtoToUpdate.FirstName,
                    LastName = entityDtoToUpdate.LastName,
                    Country = entityDtoToUpdate.Country,
                    City = entityDtoToUpdate.City,
                    State = entityDtoToUpdate.State,
                    ZipCode = entityDtoToUpdate.ZipCode,
                    ModifiedAt = entityDtoToUpdate.ModifiedAt,
                    ModifiedBy = entityDtoToUpdate.ModifiedBy,
                    IdentityUserId = entityDtoToUpdate.IdentityUserId,
                };

                if (!_unitOfWork.UserRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
