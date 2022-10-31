using OSL.Inventory.B2.Service.DTOs.BaseDto;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class CategoryDto : BaseDto<long>, IDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
