using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data;
using System;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateInventoryUserAsync(InventoryUser entityToCreate);
    }

    public class UserRepository : IUserRepository
    {
        private readonly InventoryDbContext _context;

        public UserRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateInventoryUserAsync(InventoryUser entityToCreate)
        {
            try
            {
                _context.Users.Add(entityToCreate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
