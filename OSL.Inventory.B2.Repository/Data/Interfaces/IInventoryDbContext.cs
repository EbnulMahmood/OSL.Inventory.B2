﻿using OSL.Inventory.B2.Entity;
using System.Data.Entity;

namespace OSL.Inventory.B2.Repository.Data.Interfaces
{
    public interface IInventoryDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }
    }
}