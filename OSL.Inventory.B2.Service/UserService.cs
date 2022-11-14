using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository;
using System;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(string FirstName, string LastName, string Country, string City, string State, string ZipCode, long IdentityUserId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
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

                if (!await _repository.CreateInventoryUserAsync(entity)) throw new Exception();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
