using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System;

namespace OSL.Inventory.B2.Service.DTOs.BaseDto
{
    public abstract class BaseDto<T> : IDto
    {
        public T Id { get; set; }
        public StatusDto Status { get; set; } = StatusDto.Active;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public T CreatedBy { get; set; }
        public T ModifiedBy { get; set; } = default;

        object IDto.Id
        {
            get { return Id; }
            set { }
        }

        object IDto.CreatedBy
        {
            get { return CreatedBy; }
            set { }
        }

        object IDto.ModifiedBy
        {
            get { return ModifiedBy; }
            set { }
        }
    }
}
