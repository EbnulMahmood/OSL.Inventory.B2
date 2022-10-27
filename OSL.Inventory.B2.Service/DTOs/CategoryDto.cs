using OSL.Inventory.B2.Service.DTOs.BaseDto;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class CategoryDto : BaseDto<long>, IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
