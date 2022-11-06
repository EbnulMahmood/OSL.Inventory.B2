using OSL.Inventory.B2.Entity.Enums;
using OSL.Inventory.B2.Repository.Interfaces;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service.Extensions;
using OSL.Inventory.B2.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

        public async Task<(List<object>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start,
            int length, string order, string orderDir, string searchByName, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listProductsTuple = await _unitOfWork.ProductRepository.ListProductsWithSortingFilteringPagingAsync(start, length, order, orderDir,
                    searchByName, (Status)filterByStatusDto);

                int totalRecord = listProductsTuple.Item2;
                int filterRecord = listProductsTuple.Item3;
                var listProductsDto = listProductsTuple.Item1.ConvertToDto();

                List<object> entitiesList = new List<object>();
                foreach (var item in listProductsDto)
                {
                    List<string> dataItems = new List<string>
                    {
                        item.Name,
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
                var entity = entityDtoToCreate.ConvertToEntity();
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = 1;

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
