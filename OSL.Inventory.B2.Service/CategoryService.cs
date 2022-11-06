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
    public sealed class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IDictionary<string, string> ValidateCategoryDtoService(CategoryDto entityDto)
        {
            Guard.AgainstNullParameter(entityDto, nameof(entityDto));

            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (entityDto.Name.Trim().Length == 0)
                errors.Add("Name", "Name is required.");
            if (entityDto.Description.Trim().Length == 0)
                errors.Add("Description", "Description is required.");
            return errors;
        }

        public async Task<(List<object>, int, int)> ListCategoriesWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchByName, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listCategoriesTuple = await _unitOfWork.CategoryRepository.ListCategoriesWithSortingFilteringPagingAsync(start, length, order, orderDir,
                    searchByName, (Status)filterByStatusDto);

                int totalRecord = listCategoriesTuple.Item2;
                int filterRecord = listCategoriesTuple.Item3;
                var listCategoriesDto = listCategoriesTuple.Item1.ConvertToDto();

                List<object> entitiesList = new List<object>();
                foreach (var item in listCategoriesDto)
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

        public async Task<CategoryDto> GetCategoryByIdServiceAsync(long? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.CategoryRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = entity.ConvertToDto();
                return entityDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CreateCategoryServiceAsync(CategoryDto entityDtoToCreate)
        {
            try
            {
                var entity = entityDtoToCreate.ConvertToEntity();
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = 1;

                if (!_unitOfWork.CategoryRepository.CreateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateCategoryServiceAsync(CategoryDto entityDtoToUpdate)
        {
            try
            {
                var entity = entityDtoToUpdate.ConvertToEntity();
                entity.ModifiedAt = DateTime.Now;
                entity.ModifiedBy = 2;

                if (!_unitOfWork.CategoryRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCategoryByIdServiceAsync(long entityDtoToDeleteId)
        {
            try
            {
                if (!await _unitOfWork.CategoryRepository.SoftDeleteEntity(entityDtoToDeleteId)) return false;
                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
