using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Repository.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Data
{
    public class InventoryDbContext : DbContext, IInventoryDbContext
    {
        public InventoryDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
