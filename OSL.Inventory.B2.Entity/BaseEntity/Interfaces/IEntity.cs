﻿using OSL.Inventory.B2.Entity.Enums;
using System;

namespace OSL.Inventory.B2.Entity.BaseEntity.Interfaces
{
    public interface IEntity
    {
        object Id { get; set; }
        Status Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        object CreatedBy { get; set; }
        object ModifiedBy { get; set; }
    }
}
