﻿using OSL.Inventory.B2.Entity;
using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service
{
    public interface IProductService
    {
        Task<bool> CreateProductServiceAsync(ProductDto entityDtoToCreate);
        Task<bool> DeleteProductByIdServiceAsync(long entityDtoToDeleteId);
        Task<ProductDto> GetProductByIdServiceAsync(long? entityDtoToGetId);
        Task<(IEnumerable<ProductDatatableViewDto>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start, int length, string order, 
            string orderDir, string searchByName, string filterByCategory, StatusDto filterByStatusDto = 0);
        Task<bool> UpdateProductServiceAsync(ProductDto entityDtoToUpdate);
        List<CategoryDto> SelectCategoriesListItems();
        Task<IEnumerable<CategoryDto>> ListCategoriesAsync();
        Task<object> ListCategoriesServiceAsync(string name, int page
            , int resultCount);
        Task<List<CategoryDto>> ListCategoriesByNameServiceAsync(string name);
    }

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region SingleInstance

        public async Task<ProductDto> GetProductByIdServiceAsync(long? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.ProductRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = new ProductDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    ImageUrl = entity.ImageUrl,
                    Limited = entity.Limited,
                    InStock = entity.InStock,
                    PricePerUnit = entity.PricePerUnit,
                    BasicUnit = (BasicUnitDto)entity.BasicUnit,
                    CategoryId = entity.CategoryId,
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

        public async Task<object> ListCategoriesServiceAsync(string name, int page
            , int resultCount)
        {
            var entitiesTupe = await _unitOfWork.ProductRepository
                                    .ListCategoriesAsync(name, page, resultCount);

            var results = (from c in entitiesTupe.Item1
                           select new CategorySearchDto()
                           {
                               id = c.Id,
                               text = c.Name,
                           }).ToArray();

            bool more = entitiesTupe.Item2;
            var pagination = new { more = more };

            return new
            {
                results,
                pagination,
            };
        }

        public async Task<IEnumerable<CategoryDto>> ListCategoriesAsync()
        {
            try
            {
                var entities = await _unitOfWork.CategoryRepository.ListEntitiesAsync();

                return from category in entities
                       select new CategoryDto
                       {
                           Id = category.Id,
                           Name = category.Name,
                       };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CategoryDto>> ListCategoriesByNameServiceAsync(string name)
        {
            try
            {
                var entities = await _unitOfWork.CategoryRepository
                                                   .ListCategoriesByNameAsync(name);
                var entitiesDto = (from category in entities
                                   select new CategoryDto()
                                   {
                                       Id = category.Id,
                                       Name = category.Name
                                   }).ToList();

                
                List<object> list = new List<object>();
                foreach (var item in entitiesDto)
                {
                    List<string> dataItems = new List<string>
                    {
                        item.Id.ToString(),
                        item.Name,
                    };
                    list.Add(dataItems);    
                }

                return entitiesDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CategoryDto> SelectCategoriesListItems()
        {
            var categories = _unitOfWork.CategoryRepository.ListCategoriesDropdown();
            var categoriesDto = (from category in categories
                                select new CategoryDto()
                                {
                                    Id = category.Id,
                                    Name = category.Name,
                                }).ToList();
            return categoriesDto;
        }

        public async Task<(IEnumerable<ProductDatatableViewDto>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start,
            int length, string order, string orderDir, string searchByName, string filterByCategory, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listProductsTuple = await _unitOfWork.ProductRepository.ListProductsWithSortingFilteringPagingAsync(start, length, 
                    order, orderDir, searchByName, filterByCategory, (Status)filterByStatusDto);

                int totalRecord = listProductsTuple.Item2;
                int filterRecord = listProductsTuple.Item3;
                var listProductsDto = listProductsTuple.Item1.ConvertToDto();

                // List<object> entitiesList = new List<object>();
                // foreach (var item in listProductsDto)
                // {
                //     List<string> dataItems = new List<string>
                //     {
                //         item.Name,
                //         item.InStockString,
                //         item.PricePerUnitString,
                //         item.StatusHtml,
                //         item.ActionLinkHtml
                //     };

                //     entitiesList.Add(dataItems);
                // }

                // return (entitiesList, totalRecord, filterRecord);
                return (listProductsDto, totalRecord, filterRecord);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Operations
        public async Task<bool> CreateProductServiceAsync(ProductDto entityDtoToCreate)
        {
            try
            {
                var entity = new Product
                {
                    Name = entityDtoToCreate.Name,
                    Description = entityDtoToCreate.Description,
                    ImageUrl = entityDtoToCreate.ImageUrl,
                    Limited = entityDtoToCreate.Limited,
                    InStock = entityDtoToCreate.InStock,
                    PricePerUnit = entityDtoToCreate.PricePerUnit,
                    BasicUnit = (BasicUnit)entityDtoToCreate.BasicUnit,
                    CategoryId = entityDtoToCreate.CategoryId,
                    Status = Status.Active,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                };

                if (!_unitOfWork.ProductRepository.CreateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateProductServiceAsync(ProductDto entityDtoToUpdate)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetEntityByIdAsync(entityDtoToUpdate.CategoryId);
                var entity = new Product
                {
                    Id = entityDtoToUpdate.Id,
                    Name = entityDtoToUpdate.Name,
                    Description = entityDtoToUpdate.Description,
                    ImageUrl = entityDtoToUpdate.ImageUrl,
                    Limited = entityDtoToUpdate.Limited,
                    InStock = entityDtoToUpdate.InStock,
                    PricePerUnit = entityDtoToUpdate.PricePerUnit,
                    BasicUnit = (BasicUnit)entityDtoToUpdate.BasicUnit,
                    CategoryId = entityDtoToUpdate.CategoryId,
                    Category = category,
                    Status = (Status)entityDtoToUpdate.Status,
                    CreatedAt = entityDtoToUpdate.CreatedAt,
                    CreatedBy = entityDtoToUpdate.CreatedBy,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = 2,
                };

                if (!_unitOfWork.ProductRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteProductByIdServiceAsync(long entityDtoToDeleteId)
        {
            try
            {
                if (!await _unitOfWork.ProductRepository.SoftDeleteEntity(entityDtoToDeleteId)) return false;
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
