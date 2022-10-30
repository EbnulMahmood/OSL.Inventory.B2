using OSL.Inventory.B2.Repository.Interfaces;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.Extensions;
using OSL.Inventory.B2.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<CategoryDto>> ListCategoriesServiceAsync()
        {
            try
            {
                var entities = await _unitOfWork.CategoryRepository.ListEntitiesAsync();
                if (!entities.Any()) return null;

                var entitiesDto = entities.ConvertToDto();
                return entitiesDto;
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

                if (!_unitOfWork.CategoryRepository.CreateEntity(entity)) throw new Exception();

                await _unitOfWork.SaveAsync();
                return true;
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

                if (!_unitOfWork.CategoryRepository.UpdateEntity(entity)) throw new Exception();

                await _unitOfWork.SaveAsync();
                return true;
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
                var entity = await _unitOfWork.CategoryRepository.GetEntityByIdAsync(entityDtoToDeleteId);
                if (entity == null) throw new Exception();

                if (!await _unitOfWork.CategoryRepository.DeleteEntityAsync(entity)) return false;
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
