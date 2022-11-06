using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.Interfaces
{
    public interface ICategoryService
    {
        IDictionary<string, string> ValidateCategoryDtoService(CategoryDto entityDto);
        Task<(List<object>, int, int)> ListCategoriesWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchByName, StatusDto filterByStatusDto = 0);
        Task<CategoryDto> GetCategoryByIdServiceAsync(long? entityDtoToGetId);
        Task<bool> CreateCategoryServiceAsync(CategoryDto entityDtoToCreate);
        Task<bool> UpdateCategoryServiceAsync(CategoryDto entityDtoToUpdate);
        Task<bool> DeleteCategoryByIdServiceAsync(long entityDtoToDeleteId);
    }
}