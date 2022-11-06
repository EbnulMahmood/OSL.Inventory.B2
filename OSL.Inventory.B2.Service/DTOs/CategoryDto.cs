using OSL.Inventory.B2.Service.DTOs.BaseDto;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class CategoryDto : BaseDto<long>, IDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
    }
}
