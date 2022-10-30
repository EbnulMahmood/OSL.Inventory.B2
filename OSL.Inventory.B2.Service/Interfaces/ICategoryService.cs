using OSL.Inventory.B2.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryServiceAsync(CategoryDto entityDtoToCreate);
        Task<bool> DeleteCategoryByIdServiceAsync(long entityDtoToDeleteId);
        Task<CategoryDto> GetCategoryByIdServiceAsync(long? entityDtoToGetId);
        Task<IEnumerable<CategoryDto>> ListCategoriesServiceAsync();
        Task<bool> UpdateCategoryServiceAsync(CategoryDto entityDtoToUpdate);
        IDictionary<string, string> ValidateCategoryDtoService(CategoryDto entityDto);
    }
}