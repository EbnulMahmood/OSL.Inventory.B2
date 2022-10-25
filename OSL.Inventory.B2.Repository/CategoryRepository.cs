using OSL.Inventory.B2.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository
{
    public class CategoryRepository
    {
        private readonly InventoryDbContext _context;
        public CategoryRepository()
        {
            _context = new InventoryDbContext();
        }


    }
}
