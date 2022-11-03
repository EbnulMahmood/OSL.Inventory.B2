using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductServiceAsync(ProductDto entityDtoToCreate);
        Task<bool> DeleteProductByIdServiceAsync(long entityDtoToDeleteId);
        Task<ProductDto> GetProductByIdServiceAsync(long? entityDtoToGetId);
        Task<(IEnumerable<ProductDto>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start, int length, string order, string orderDir, string searchByName, StatusDto filterByStatusDto = 0);
        Task<bool> UpdateProductServiceAsync(ProductDto entityDtoToUpdate);
        IDictionary<string, string> ValidateProductDtoService(ProductDto entityDto);
    }
}