using OSL.Inventory.B2.Service.DTOs.BaseDto;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class ProductViewDto : BaseDto<long>, IDto
    {
        public string Name { get; set; } = string.Empty;
        public string InStockString { get; set; }
        public string PricePerUnitString { get; set; }
        public string StatusHtml { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
    }
}
