﻿using OSL.Inventory.B2.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces
{
    public interface IDto
    {
        object Id { get; set; }
        Status Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        object CreatedBy { get; set; }
        object ModifiedBy { get; set; }
    }
}