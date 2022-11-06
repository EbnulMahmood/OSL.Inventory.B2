using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using OSL.Inventory.B2.Service.DTOs.BaseDto;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class ProductDto : BaseDto<long>, IDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Limited { get; set; }
        public int InStock { get; set; }
        public decimal PricePerUnit { get; set; }
        public string BasicUnit { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
    }
}
