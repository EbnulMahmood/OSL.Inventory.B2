﻿using System.Threading.Tasks;

namespace OSL.Inventory.B2.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        void Dispose();
        Task<bool> SaveAsync();
    }
}