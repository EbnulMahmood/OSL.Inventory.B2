using OSL.Inventory.B2.Entity;
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
        Task<(List<object>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start, int length, string order, 
            string orderDir, string searchByName, string filterByCategory, StatusDto filterByStatusDto = 0);
        Task<bool> UpdateProductServiceAsync(ProductDto entityDtoToUpdate);
        IDictionary<string, string> ValidateProductDtoService(ProductDto entityDto);
        List<CategoryDto> SelectCategoriesListItems();
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
        #endregion

        #region ListInstance
        #endregion

        #region Operations
        #endregion

        public IDictionary<string, string> ValidateProductDtoService(ProductDto entityDto)
        {
            Guard.AgainstNullParameter(entityDto, nameof(entityDto));

            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (entityDto.Name.Trim().Length == 0)
                errors.Add("Name", "Name is required.");
            if (entityDto.Description.Trim().Length == 0)
                errors.Add("Description", "Description is required.");
            return errors;
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

        public async Task<(List<object>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start,
            int length, string order, string orderDir, string searchByName, string filterByCategory, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listProductsTuple = await _unitOfWork.ProductRepository.ListProductsWithSortingFilteringPagingAsync(start, length, 
                    order, orderDir, searchByName, filterByCategory, (Status)filterByStatusDto);

                int totalRecord = listProductsTuple.Item2;
                int filterRecord = listProductsTuple.Item3;
                var listProductsDto = listProductsTuple.Item1.ConvertToDto();

                List<object> entitiesList = new List<object>();
                foreach (var item in listProductsDto)
                {
                    List<string> dataItems = new List<string>
                    {
                        item.Name,
                        item.InStockString,
                        item.PricePerUnitString,
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

        public async Task<ProductDto> GetProductByIdServiceAsync(long? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.ProductRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = entity.ConvertToDto();
                return entityDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

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
                var entity = entityDtoToUpdate.ConvertToEntity();
                entity.ModifiedAt = DateTime.Now;
                entity.ModifiedBy = 2;

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
    }
}
