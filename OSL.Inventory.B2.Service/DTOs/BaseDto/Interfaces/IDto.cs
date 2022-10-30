using OSL.Inventory.B2.Service.DTOs.Enums;
using System;

namespace OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces
{
    public interface IDto
    {
        object Id { get; set; }
        StatusDto Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        object CreatedBy { get; set; }
        object ModifiedBy { get; set; }
    }
}
