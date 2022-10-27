using OSL.Inventory.B2.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.Interfaces
{
    public interface ICategoryService
    {
        IDictionary<string, string> ValidateCategoryDtoService(CategoryDto entityDto);
        Task<IEnumerable<CategoryDto>> ListCategoriesServiceAsync();
        Task<CategoryDto> GetCategoryByIdServiceAsync(long? entityDtoToGetId);
        Task<bool> CreateCategoryServiceAsync(CategoryDto entityDtoToCreate);
        Task<bool> UpdateCategoryServiceAsync(CategoryDto entityDtoToUpdate);
        Task<bool> DeleteCategoryByIdServiceAsync(long entityDtoToDeleteId);
    }
}